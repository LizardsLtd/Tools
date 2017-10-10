module Picums.Tests.Localisation.GetAllTranslationQuery

    open System
    open NLog
    open Picums.Data.CQRS.DataAccess
    open Picums.Data.InMemory
    open Picums.Mvc.Localisation.DataStorage
    open Picums.Tests
    open FsUnit.Xunit
    open Xunit

    let context = InMemoryDataContext()
    let logger = LogManager.GetCurrentClassLogger()

    [<Fact>]
    let ``GetAllTranslationsQuery returns QueryForAll`` () =
        let query = GetAllTranslationsQuery(context, logger)
        let parts = DatabaseParts("test", "test")
        let result = query.Execute(parts)

        result |> should not' (be Null)