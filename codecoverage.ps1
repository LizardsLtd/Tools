function runOpenCover([string]$csproj){
    Write-Host "projectFile" $csproj
    $fileName = [io.path]::GetFileNameWithoutExtension($csproj)

    .\OpenCover.4.6.519\tools\OpenCover.Console.exe `
		-target:"c:\Program Files\dotnet\dotnet.exe" `
		-targetargs:"test -c Release $csproj" `
		-mergeoutput `
		-hideskipped:File `
		-output:coverage.xml `
		-oldStyle `
		-register:user `
		-filter:"+[Picums*]* -[Picums.Tests.*]*" `
		-searchdirs:test/$fileName/bin/Release/netcoreapp2.0
}

Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -o .\nuget.exe

.\nuget.exe install OpenCover -Version 4.6.519
.\nuget.exe install ReportGenerator -Version 2.5.9

if(test-path coverage){
    Remove-Item -r coverage
}

Get-ChildItem -Recurse -Filter "*.*proj" -path test  | ForEach-Object{ $_.FullName} | ForEach-Object{runOpenCover($_)}

.\ReportGenerator.2.5.9\tools\ReportGenerator.exe -reports:coverage.xml -targetdir:coverage -verbosity:Error

./coverage/index.htm