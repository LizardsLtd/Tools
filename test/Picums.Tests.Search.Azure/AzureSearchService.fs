module Tests

open System
open Xunit
open FsUnit.Xunit
open Microsoft.Spatial
open Newtonsoft.Json
open Picums.Mvc.Configuration
open Microsoft.Extensions.Logging
open FakeItEasy
open Picums.Search.Azure

type TestSearchItem() =
    interface IHasScore with
        member this.Score = 0.0

[<Fact>]
let ``SearchResult can be empy`` () =
    let result = new TestSearchItem()
    result.HasResults |> should equal false