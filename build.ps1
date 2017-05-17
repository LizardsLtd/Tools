Invoke-WebRequest -Uri https://raw.githubusercontent.com/dotnet/cli/master/scripts/obtain/dotnet-install.ps1 -OutFile dotnet-install.ps1

./dotnet-install.ps1 -Version 2.0.0-preview1-005977 -Channel preview -InstallDir dt

./dt/dotnet restore -v m
./dt/dotnet build -v m