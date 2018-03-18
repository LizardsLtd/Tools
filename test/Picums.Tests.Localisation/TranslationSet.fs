module Picums.Tests.Localisation.TranslationSet

    open System
    open Picums.Mvc.Localisation
    open FsUnit.Xunit
    open Xunit

    [<Fact>]
    let ``Translation item are matching`` () =
        let culture = System.Globalization.CultureInfo("en-gb")
        let key = "example key"
        let value = "example value"
        let first = TranslationItem(System.Guid.NewGuid(), culture, key, value)
        let second = TranslationItem(System.Guid.NewGuid(), culture, key, value)

        first |> should equal second

    [<Fact>]
    let ``Translation item are not equals for differente values`` () =
        let culture = System.Globalization.CultureInfo("en-gb")
        let key = "example key"
        let value = "example value"
        let first = TranslationItem(System.Guid.NewGuid(), culture, key, "1")
        let second = TranslationItem(System.Guid.NewGuid(), culture, key, "2")

        first |> should not' (equal second)

    [<Fact>]
    let ``Translation item compare keys method works`` () =
        let culture = System.Globalization.CultureInfo("en-gb")
        let key = "example key"
        let value = "example value"
        let first = TranslationItem(System.Guid.NewGuid(), culture, key, "example value")

        let areEquals = first.CompareKeys(culture, key)

        areEquals |> should be True