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
    let first = new  TextSearchForParameter("first")
    let second = new TextSearchForParameter("second")
    let andKeyword = new And(first, second)
    andKeyword.GetSearchCommmand() |> should equal "first && second"