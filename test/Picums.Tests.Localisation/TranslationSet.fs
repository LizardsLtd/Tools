open System
open Picums.Localisation.Data
open Picums.Tests
open Should.Fluent
open Xunit

module TranslationSet =

    [<Fact>]
    let ``Translation item are matching`` () =
        let culture = System.Globalization.CultureInfo("en-gb")
        let key = "example key"
        let value = "example value"
        let first = TranslationItem(System.Guid.NewGuid(), culture, key, value)
        let second = TranslationItem(System.Guid.NewGuid(), culture, key, value)

        first.Should().Be.Equals(second);

    [<Fact>]
    let ``Translation item are not equals for differente values`` () =
        let culture = System.Globalization.CultureInfo("en-gb")
        let key = "example key"
        let value = "example value"
        let first = TranslationItem(System.Guid.NewGuid(), culture, key, "1")
        let second = TranslationItem(System.Guid.NewGuid(), culture, key, "2")

        first.Should().Not.Be.Equals(second);

    [<Fact>]
    let ``Translation item compare keys method works`` () =
        let culture = System.Globalization.CultureInfo("en-gb")
        let key = "example key"
        let value = "example value"
        let first = TranslationItem(System.Guid.NewGuid(), culture, key, "example value")

        let areEquals = first.CompareKeys(culture, key)

        areEquals.Should().Be.True();