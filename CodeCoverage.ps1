nuget install OpenCover
.\OpenCover.*\tools\OpenCover.Console.exe 
-register:user 
-filter:"+[*]* -[FluentAssertions*]*" 
-target:"packages\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe" 
-targetargs:"/domain:single fluentOptionals.Tests/bin/debug/fluentOptionals.Tests.dll"
-output:coverage.xml