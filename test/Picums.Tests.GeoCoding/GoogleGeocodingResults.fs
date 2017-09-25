module Picums.Tests.GeoCoding.GoogleGeocodingResults

    open System
    open Picums.Tests
    open FsUnit.Xunit
    open Xunit
    open Microsoft.Extensions.Logging
    open Picums.GeoCoding
    
    let logger = 
        let factory: TestLoggerFactory = new TestLoggerFactory()
        factory.CreateLogger("null")

    let exampleFile = 
        let embededResources = new EmbeddedContentLoader("Picums.Tests.GeoCoding");
        embededResources.LoadTextFileAsync("GoogleServiceResults") 
            |> Async.AwaitTask
            |> Async.RunSynchronously
        
            
    //[<Fact>]
    //let ``Parse service result`` () =
    //    let parser = new GoogleGeocodingResults(logger, exampleFile)
    //    parser.HasResults |> should be True
