function executeXUnit($path){
	cd $path
	dotnet xunit -appveyor
	cd ../..
}

Get-ChildItem -Recurse -Filter *.*proj -path test | %{ executeXUnit($_.DirectoryName) }