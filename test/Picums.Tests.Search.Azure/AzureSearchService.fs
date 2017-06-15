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

type TestSearchItem() =
    interface IHasScore with
        member this.Score = 0.0

[<Fact>]
let ``SearchResults can be empy`` () =
    let result = SearchResults.Empty
    result.HasResults |> should equal false

[<Fact>]
let ``SearchResults has sresult when provided with data`` () =
    let result = new SearchResults<TestSearchItem>([new TestSearchItem(); new TestSearchItem()])
    result.HasResults |> should equal true

[<Fact>]
let ``SearchResults saves results correctly`` () =
    let result = new SearchResults<TestSearchItem>([new TestSearchItem(); new TestSearchItem()])
    result.Results.Count() |> should equal 2

[<Fact>]
let ``Merge between two SearchResults works`` () =
    let first = new SearchResults<TestSearchItem>([new TestSearchItem(); new TestSearchItem()])
    let second = new SearchResults<TestSearchItem>([new TestSearchItem(); new TestSearchItem()])
    let merged = first.Merge(second)
    merged.Results.Count() |> should equal 4