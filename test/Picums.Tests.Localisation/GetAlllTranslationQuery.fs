module Picums.Tests.Localisation.GetAllTranslationQuery

    open System
    open Picums.Localisation.Data
    open Picums.Data.InMemory
    open Picums.Data.CQRS.DataAccess
    open Picums.Tests
    open FsUnit.Xunit
    open Xunit

    let context = InMemoryDataContext()
    let logger = TestLoggerFactory()

    [<Fact>]
    let ``GetAllTranslationsQuery returns QueryForAll`` () =
        let query = new GetAllTranslationsQuery(context,logger)
        let result = query.GetQuery(DatabaseParts("test", "test"))

        result |> should not' (be Null)