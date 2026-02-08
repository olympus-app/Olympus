#!/bin/bash

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
REPO_ROOT="$(dirname "$SCRIPT_DIR")"

olympus() {

    local DO_MAINTENANCE=false
    local DO_CLEAN=false
    local DO_RESTORE=false
    local DO_BUILD=false
    local DO_PUBLISH=false
    local DO_PACK=false
    local DO_RUN=false
    local DO_WATCH=false
    local DO_WATCHRUN=false
    local VERBOSITY="minimal"

    local TARGET_PATH="$REPO_ROOT"
    local NUGET_OUTPUT="$HOME/.nuget/local"
    local IS_SPECIFIC_TARGET=false

    if [ $# -eq 0 ]; then DO_BUILD=true; fi

    while [[ "$#" -gt 0 ]]; do
        case $1 in
            -m|--maintenance) DO_MAINTENANCE=true ;;
            -c|--clean) DO_CLEAN=true ;;
            -e|--restore) DO_RESTORE=true ;;
            -b|--build) DO_BUILD=true ;;
            -p|--publish) DO_PUBLISH=true ;;
            -k|--pack) DO_PACK=true ;;
            -r|--run) DO_RUN=true ;;
            -w|--watch) DO_WATCH=true ;;
            -wr|--watchrun) DO_WATCHRUN=true ;;
            -v|--verbose) VERBOSITY="detailed" ;;
            *)
                if [[ "$1" = /* ]]; then
                    TARGET_PATH="$1"
                else
                    TARGET_PATH="$REPO_ROOT/$1"
                fi
                IS_SPECIFIC_TARGET=true
                ;;
        esac
        shift
    done

    set -e

    dotnet build-server shutdown > /dev/null 2>&1 || true

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_CLEAN" = true ]; then
        if [ "$IS_SPECIFIC_TARGET" = true ]; then
            dotnet clean "$TARGET_PATH" -v "$VERBOSITY"
        else
            find "$REPO_ROOT" -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
        fi
    fi

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_BUILD" = true ] || [ "$DO_PACK" = true ]; then
        if [ "$IS_SPECIFIC_TARGET" = false ]; then
            local PACKAGE_SOURCE="$REPO_ROOT/Packages"
            if [ -d "$PACKAGE_SOURCE" ]; then
                for package_dir in "$PACKAGE_SOURCE"/*; do
                    if [ -d "$package_dir" ]; then
                        package_name=$(basename "$package_dir")
                        cache_path="$HOME/.nuget/packages/${package_name,,}"
                        if [ -d "$cache_path" ]; then
                            rm -rf "$cache_path"
                        fi
                        dotnet pack "$package_dir" -c Release -o "$NUGET_OUTPUT" -v "$VERBOSITY"
                    fi
                done
            fi
        fi
    fi

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_RESTORE" = true ]; then
        if [ "$IS_SPECIFIC_TARGET" = true ]; then
            dotnet restore "$TARGET_PATH"
        else
            dotnet nuget locals all --clear
            dotnet workload restore "$REPO_ROOT/Olympus.slnx"
            if [ -f "$REPO_ROOT/.config/dotnet-tools.json" ]; then
                dotnet tool restore --tool-manifest "$REPO_ROOT/.config/dotnet-tools.json"
            fi
            dotnet restore "$REPO_ROOT"
        fi
    fi

    dotnet build-server shutdown > /dev/null 2>&1 || true

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_BUILD" = true ]; then
        dotnet build "$TARGET_PATH" -c Debug -v "$VERBOSITY"
    fi

    if [ "$DO_PUBLISH" = true ]; then
        dotnet publish "$TARGET_PATH" -c Release -v "$VERBOSITY"
    fi

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_PACK" = true ]; then
        dotnet pack "$TARGET_PATH" -c Release -o "$NUGET_OUTPUT" -v "$VERBOSITY"
    fi

    if [ "$DO_RUN" = true ]; then
        dotnet run --project "$TARGET_PATH" -c Debug -v "$VERBOSITY"
    fi

    if [ "$DO_WATCH" = true ]; then
        dotnet watch --project "$TARGET_PATH" -v "$VERBOSITY"
    fi

    if [ "$DO_WATCHRUN" = true ]; then
        dotnet watch --project "$TARGET_PATH" run -c Debug -v "$VERBOSITY"
    fi

    set +e

}

alias oly="olympus"
alias o="olympus"

olympus_autocomple() {

    local cur opts
    COMPREPLY=()
    cur="${COMP_WORDS[COMP_CWORD]}"

    if [[ "$cur" == -* ]]; then
        opts="-m --maintenance -c --clean -e --restore -b --build -p --publish -k --pack -r --run -w --watch -wr --watchrun -v --verbose"
        COMPREPLY=( $(compgen -W "${opts}" -- ${cur}) )
        return 0
    else
        cd "$REPO_ROOT" && COMPREPLY=( $(compgen -d -- "$cur") ) && cd - > /dev/null
        return 0
    fi

}

complete -F olympus_autocomple olympus oly o
