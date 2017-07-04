module Picums.Tests.Search.Keywords

open System
open System.Linq
open Xunit
open FsUnit.Xunit
open Microsoft.Spatial
open Newtonsoft.Json
open Microsoft.Extensions.Logging
open FakeItEasy
open Picums.Search.Azure.KeyWords

[<Fact>]
let ``And keyword should merge searchParameters properly now`` () =
    let first = new  FuzzyMatchKeyword("first")
    let second = new FuzzyMatchKeyword("second")
    let andKeyword = new And(first, second)
    andKeyword.GetSearchCommmand() |> should equal "search=first*&search=second*"

[<Fact>]
let ``Location keyword can work with locale using comma to separate double`` () =
    let keyword = new LocationKeyword(10.0, 10.0, "Location", 25)
    let result = keyword.GetSearchCommmand()
    result |> should equal "$filter=geo.distance(Location, geography'POINT(10 10)') lt 25"