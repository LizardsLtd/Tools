function executeXUnit($path){
	cd $path
	dotnet xunit
	cd ../..
}

executeXUnit("test\Picums.Data.Azure.Tests")
executeXUnit("test\Picums.GeoCoding.Tests")
executeXUnit("test\Picums.Maybe.Tests")
executeXUnit("test\Picums.Mvc.Tests")
executeXUnit("test\Picums.Tests.Localisation")
executeXUnit("test\Picums.Data.Tests")