nuget install OpenCover -version 4.6.519

$projects = get-item ".\Tests\*\project.json" |
    foreach { $_.FullName }

$command = $projects -join " " 
    
.\OpenCover.4.6.519\tools\OpenCover.Console.exe `
    "-target:C:\Program Files\dotnet\dotnet.exe" `
    "-targetargs:test $($command)" `
    -register:user `
    "-filter:+[*]* -[xunit*]*" `
    -oldStyle `
    -output:coverage.xml
