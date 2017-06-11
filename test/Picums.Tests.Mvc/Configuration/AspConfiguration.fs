module Picums.Tests.Mvc.Configuration

open System
open Xunit
open FsUnit.Xunit
open Microsoft.Spatial
open Newtonsoft.Json
open Picums.Mvc.Configuration
open Microsoft.Extensions.Logging
open FakeItEasy

let mutable testCount = 0

[<Fact>]
let ``Can add action to AspConfig `` () =
    let configItem =  new AspConfigurator()
    let providerMock = A.Fake<ILoggerProvider>()
    configItem.Add(fun app env logger -> logger.AddProvider(providerMock))