module Picums.Tests.GeoCoding.GoogleGeocodingService

    open System
    open Picums.Tests
    open FsUnit.Xunit
    open Xunit
    open Microsoft.Extensions.Logging
    open Picums.GeoCoding
    open Picums.Maybe
    open Microsoft.Spatial

    let service: GoogleGeocodingService = new GoogleGeocodingService(NLog.LogManager.GetCurrentClassLogger());

    [<Fact>]
    let ``Connect to service`` () =
        let result: Maybe<GeographyPoint> =
            service.GeocodeAsync("10", "Downing street", "London", "UK")
                |> Async.AwaitTask
                |> Async.RunSynchronously

        result.Value.Latitude |> should equal  51.5033635
        result.Value.Longitude |> should equal -0.1276248

    [<Fact>]
    let ``Try to check known address`` () =
        let result =
            service.GeocodeAsync("5", "Kochanowskiego", "", "Zgorzelec", "Dolnoslaskie", "poland", "50-900")
                |> Async.AwaitTask
                |> Async.RunSynchronously

        result.Value.Latitude |> should equal 51.1287637
        result.Value.Longitude |> should equal 15.0137526

    [<Fact>]
    let ``Try to check non existing address`` () =
        let result =
            service.GeocodeAsync("", "", "")
                |> Async.AwaitTask
                |> Async.RunSynchronously

        result.IsNone |> should be True