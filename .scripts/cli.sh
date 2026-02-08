#!/bin/bash

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
REPOSITORY_PATH="$(dirname "$SCRIPT_DIR")"

olympus() {

    local DO_MAINTENANCE=false
    local DO_CLEAN=false
    local DO_CLEAR=false
    local DO_RESTORE=false
    local DO_BUILD=false
    local DO_PUBLISH=false
    local DO_PACK=false
    local DO_RUN=false
    local DO_WATCH=false
    local DO_WATCHRUN=false
    local VERBOSITY="minimal"

    local TARGET_PATH="$REPOSITORY_PATH"
    local NUGET_OUTPUT="$HOME/.nuget/local"
    local IS_SPECIFIC_TARGET=false

	local SOLUTION_FILE=$(find "$REPOSITORY_PATH" -maxdepth 1 -name "*.slnx" -print -quit)
    local SOLUTION_NAME=""

	if [ -n "$SOLUTION_FILE" ]; then

		SOLUTION_NAME=$(basename "$SOLUTION_FILE" .slnx)

	fi

    if [ $# -eq 0 ]; then

		DO_BUILD=true;

	fi

    while [[ "$#" -gt 0 ]]; do

        case $1 in
          	maintenance|m) DO_MAINTENANCE=true ;;
            clean|c) DO_CLEAN=true ;;
            clear|cr) DO_CLEAR=true ;;
            restore|re) DO_RESTORE=true ;;
            build|b) DO_BUILD=true ;;
            publish|p) DO_PUBLISH=true ;;
            pack|pk) DO_PACK=true ;;
            run|r) DO_RUN=true ;;
            watch|w) DO_WATCH=true ;;
            watchrun|wr) DO_WATCHRUN=true ;;
            -v|--verbose) VERBOSITY="detailed" ;;
            *)
                if [[ "$1" = /* ]]; then
                    TARGET_PATH="$1"
                else
                    TARGET_PATH="$REPOSITORY_PATH/$1"
                fi
                IS_SPECIFIC_TARGET=true
                ;;
        esac

        shift

    done

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_CLEAN" = true ]; then

        dotnet clean "$TARGET_PATH" -v "$VERBOSITY"

    fi

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_CLEAR" = true ]; then

        dotnet build-server shutdown > /dev/null 2>&1 || true

        find "$TARGET_PATH" -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

        if [ "$DO_RESTORE" = true ]; then

            dotnet nuget locals all --clear

        fi

    fi

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_RESTORE" = true ] || [ "$DO_BUILD" = true ] || [ "$DO_PUBLISH" = true ] || [ "$DO_PACK" = true ]; then

        if ! dotnet nuget list source | grep -q "local"; then

            mkdir -p "$NUGET_OUTPUT"
            dotnet nuget add source "$NUGET_OUTPUT" -n local

        fi

        local PACKAGE_SOURCE="$REPOSITORY_PATH/Packages"

        if [ -d "$PACKAGE_SOURCE" ]; then

            for package_dir in "$PACKAGE_SOURCE"/*; do

                if [ -d "$package_dir" ]; then

                    package_name=$(basename "$package_dir")
                    cache_path="$HOME/.nuget/packages/${package_name,,}"

                    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_RESTORE" = true ]; then
                        if [ -d "$cache_path" ]; then
                            rm -rf "$cache_path"
                        fi
                    fi

                    local SHOULD_PACK_NOW=false

                    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_RESTORE" = true ]; then

                        SHOULD_PACK_NOW=true

                    elif [ "$DO_BUILD" = true ] || [ "$DO_PUBLISH" = true ] || [ "$DO_PACK" = true ]; then

                        if ! find "$NUGET_OUTPUT" -maxdepth 1 -name "${package_name}*.nupkg" -print -quit | grep -q .; then

                            SHOULD_PACK_NOW=true

                        fi

                    fi

                    if [ "$SHOULD_PACK_NOW" = true ]; then

                        dotnet pack "$package_dir" -c Release -o "$NUGET_OUTPUT" -v "$VERBOSITY"

                    fi

                fi

            done

        fi

    fi

    if [ "$DO_MAINTENANCE" = true ] || [ "$DO_RESTORE" = true ]; then

        if [ "$IS_SPECIFIC_TARGET" = true ]; then

            dotnet restore "$TARGET_PATH"

        else

            dotnet workload restore "$SOLUTION_FILE"
            dotnet tool restore --tool-manifest "$REPOSITORY_PATH/.config/dotnet-tools.json"
            dotnet restore "$REPOSITORY_PATH"

        fi

    fi

    if [ "$DO_BUILD" = true ]; then

        dotnet build "$TARGET_PATH" -c Debug -v "$VERBOSITY"

    fi

    if [ "$DO_PUBLISH" = true ]; then

        dotnet publish "$TARGET_PATH" -c Release -v "$VERBOSITY"

    fi

    if [ "$DO_PACK" = true ]; then

        dotnet pack "$TARGET_PATH" -c Release -o "$NUGET_OUTPUT" -v "$VERBOSITY"

    fi

    if [ "$DO_WATCH" = true ] && [ "$DO_RUN" = true ]; then

        DO_WATCHRUN=true

    fi

	local BUILD_CONFIG="Debug"

    if [ "$DO_PUBLISH" = true ]; then

        BUILD_CONFIG="Release"

    fi

	if [ "$DO_RUN" = true ] || [ "$DO_WATCH" = true ] || [ "$DO_WATCHRUN" = true ]; then

		if [ "$IS_SPECIFIC_TARGET" = false ]; then

			local ASPIRE_PATH_1="$REPOSITORY_PATH/Aspire/$SOLUTION_NAME.Aspire.Host"
        	local ASPIRE_PATH_2="$REPOSITORY_PATH/$SOLUTION_NAME.Aspire.Host"

			if [ -d "$ASPIRE_PATH_1" ]; then

				TARGET_PATH="$ASPIRE_PATH_1"

			elif [ -d "$ASPIRE_PATH_2" ]; then

				TARGET_PATH="$ASPIRE_PATH_2"

			fi

		fi

		if [ "$DO_WATCHRUN" = true ]; then

			dotnet watch --project "$TARGET_PATH" run -c "$BUILD_CONFIG" -v "$VERBOSITY"

		elif [ "$DO_RUN" = true ]; then

			dotnet run --project "$TARGET_PATH" -c "$BUILD_CONFIG" -v "$VERBOSITY"

		elif [ "$DO_WATCH" = true ]; then

			dotnet watch --project "$TARGET_PATH" -c "$BUILD_CONFIG" -v "$VERBOSITY"

		fi

	fi

}

alias oly="olympus"
alias o="olympus"

olympus_autocomple() {

    local cur opts
    COMPREPLY=()
    cur="${COMP_WORDS[COMP_CWORD]}"

	opts="maintenance m clean c clear cr restore re build b publish p pack pk run r watch w watchrun wr --verbose -v"

    local commands=$(compgen -W "${opts}" -- ${cur})
    local dirs=""

    if cd "$REPOSITORY_PATH" 2>/dev/null; then
        dirs=$(compgen -d -- "$cur")
        cd - > /dev/null
    fi

    COMPREPLY=( $commands $dirs )
    return 0

}

complete -F olympus_autocomple olympus oly o
