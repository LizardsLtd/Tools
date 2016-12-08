param(
    [string] $outputPath  
)

function DownloadNugets(){
    wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
    .\nuget.exe install OpenCover -version 4.6.519
    .\nuget.exe install OpenCoverToCoberturaConverter -version 0.2.4
}

function RunOpenCover(){
    $projects = get-item ".\Tests\*\project.json" |
        foreach { $_.FullName }

    $command = $projects -join " " 


    .\OpenCover.4.6.519\tools\OpenCover.Console.exe `
        "-target:C:\Program Files\dotnet\dotnet.exe" `
        "-targetargs:test $($command)" `
        -register:user `
        "-filter:+[TheLizzards*]* -[Microsoft*]* -[xunit*]*" `
        -oldStyle `
        "-output:coverage-report.xml"
}

function ConvertToCobertura(){
    .\OpenCoverToCoberturaConverter.0.2.4.0\tools\OpenCoverToCoberturaConverter.exe "-input:coverage-report.xml" "-output:$coverageReport"
}

DownloadNugets
RunOpenCover
ConvertToCobertura
