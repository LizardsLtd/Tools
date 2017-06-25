module AutoDiscovery

open System
open Xunit
open FsUnit.Xunit
open Microsoft.Spatial
open Newtonsoft.Json
open Picums.Mvc.Configuration
open Microsoft.Extensions.Logging
open FakeItEasy

//[<Fact>]
//let ``Automatic configueration for CommandHandler works`` () =
//    let services = A.Fake<IServiceCollection>()
//    let host = new
//    let configurationDefault = new CQRSDefaults()
//    configurationDefault.Apply();