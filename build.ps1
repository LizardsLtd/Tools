Invoke-WebRequest -Uri https://raw.githubusercontent.com/dotnet/cli/master/scripts/obtain/dotnet-install.ps1 -OutFile dotnet-install.ps1

./dotnet-install.ps1 -Version 2.0.0-preview2-006082 -Channel preview

dotnet restore -v m
dotnet build -v m
Get-ChildItem -Recurse -Filter *.csproj -path src | %{ dotnet pack $_ --include-symbols --include-source -c debug}