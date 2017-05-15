module TranslationSet = 
    open System
    open Picums.Localisation.Data
    open Picums.Tests
    open Should
    open Xunit

    let culture = System.Globalization.CultureInfo("en-gb")
    let key = "example key"
    let value = "example value"

    [<Fact>]
    let ``Translation item are matching`` () =
        let first = TranslationItem(culture, key, value)
        let second = TranslationItem(culture, key, value)

        let areEquals = first.Equals(second)

        areEquals.ShouldBeTrue();

    [<Fact>]
    let ``Translation item are not equals for differente values`` () =
        let first = TranslationItem(culture, key, "1")
        let second = TranslationItem(culture, key, "2")

        let areEquals = first.Equals(second)

        areEquals.ShouldBeFalse();

    [<Fact>]
    let ``Translation item compare keys method works`` () =
        let first = TranslationItem(culture, key, "example value")

        let areEquals = first.CompareKeys(culture, key)

        areEquals.ShouldBeTrue();
