module Picums.Tests.Search.Service

open System
open System.Linq
open Xunit
open FsUnit.Xunit
open Microsoft.Spatial
open Newtonsoft.Json
open Microsoft.Extensions.Logging
open FakeItEasy
open Picums.Search.Azure.KeyWords

//[<Fact>]
//let ``Progressive aproach to Serach service works as expected.`` () =
//    let service = new AzureSer