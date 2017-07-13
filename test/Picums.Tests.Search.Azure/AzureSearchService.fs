module Picums.Tests.Search.Azure

open System
open System.Linq
open Xunit
open FsUnit.Xunit
open Microsoft.Spatial
open Newtonsoft.Json
open Microsoft.Extensions.Logging
open FakeItEasy
open Picums.Search.Azure

type TestSearchItem(Score: double) =
    interface IHasScore with
        member this.Score = Score

[<Fact>]
let ``SearchResults can be empy`` () =
    let result = SearchResults.Empty
    result.HasResults |> should equal false

[<Fact>]
let ``SearchResults has results when provided with data`` () =
    let result = new SearchResults<TestSearchItem>([new TestSearchItem(1.0); new TestSearchItem(2.0)])
    result.HasResults |> should equal true

[<Fact>]
let ``SearchResults saves results correctly`` () =
    let result = new SearchResults<TestSearchItem>([new TestSearchItem(1.0); new TestSearchItem(2.0)])
    result.Results.Count() |> should equal 2

[<Fact>]
let ``Merge between two SearchResults works`` () =
    let first = new SearchResults<TestSearchItem>([new TestSearchItem(1.0); new TestSearchItem(2.0)])
    let second = new SearchResults<TestSearchItem>([new TestSearchItem(3.0); new TestSearchItem(4.0)])
    let merged = first.Merge(second)
    merged.Results.Count() |> should equal 4

[<Fact>]
let ``Search reslt could be sorted`` () =
    let results = new SearchResults<TestSearchItem>([new TestSearchItem(3.0); new TestSearchItem(2.0)])
    let orderedResults = results.OrderByScore()
    orderedResults.Results.Count() |> should equal 2
    (orderedResults.Results.First() :> IHasScore).Score  |> should equal 2.0
    (orderedResults.Results.Last() :> IHasScore).Score  |> should equal 3.0