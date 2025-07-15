param (
	[Parameter(Mandatory = $true)][string]$Name,
	[Parameter(Mandatory = $false)][Switch]$Update,
	[Parameter(Mandatory = $false)][Switch]$Force,
	[Parameter(Mandatory = $false)][Switch]$Drop,
	[Parameter(Mandatory = $false)][Switch]$Run
)

dotnet build
if ($Drop) { dotnet ef database drop --project Server --force --no-build }
if ($Force) { Remove-Item -Path "Server/Database/Migrations" -Recurse -Force -ErrorAction SilentlyContinue }
dotnet ef migrations add $Name --project Server --output-dir Database/Migrations
if ($Update) { dotnet ef database update --project Server }
if ($Run) { dotnet run --project Server --no-build }
