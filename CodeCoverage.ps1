nuget install OpenCover -version 4.6.519

get-item ".\Tests\*\project.json" |
 foreach { 
    .\OpenCover.4.6.519\tools\OpenCover.Console.exe `
        "-target:C:\Program Files\dotnet\dotnet.exe" `
        "-targetargs:test $_" `
        -register:user `
        "-filter:+[*]* -[xunit*]*" `
        -oldStyle `
        -output:coverage.$($_.Directory.Name).xml ` 
 }
