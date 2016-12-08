param(
    [string] $configuration,
    [string] $outputLocation,
    [string] $packageVersion
)

dotnet restore
Get-ChildItem "src/*/project.json" | % { dotnet pack $_.FullName -c $configuration -o $outputLocation --version-suffix $packageVersion -s}