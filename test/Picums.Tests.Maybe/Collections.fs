module Picums.Tests.Maybe.Collections

    open System
    open System.Linq
    open Picums.Maybe
    open Picums.Tests
    open Xunit
    open FsUnit.Xunit

    let exampleCollection = [1; 2; 3; 4; 5]

    [<Fact>]
    let ``Cast To Maybe Collection Works`` () =
        let collection = exampleCollection.ToMaybeList()
        collection.All(fun x -> x.IsSome) |> should be True

    [<Fact>]
    let ``SingleOrNothionglet `` () =
        let collection = [1;]
        let result = collection.SingleOrNothing();
        result.IsSome |> should be True

    [<Fact>]
    let ``SingleOrNothingWoithQuerylet `` () =
        let result = exampleCollection.SingleOrNothing(fun x -> x = 1);
        result.IsSome |> should be True

    [<Fact>]
    let ``FirstOrNothionglet `` () =
        let collection = [1;]
        let result = collection.FirstOrNothing();
        result.IsSome |> should be True

    [<Fact>]
    let ``FirstOrNothingWoithQuerylet `` () =
        let result = exampleCollection.FirstOrNothing(fun x -> x = 1);
        result.IsSome |> should be True

    [<Fact>]
    let ``LastOrNothionglet `` ()=
        let collection = [1;]
        let result = collection.LastOrNothing();
        result.IsSome |> should be True

    [<Fact>]
    let ``LastOrNothingWoithQuerylet `` () =
        let result = exampleCollection.LastOrNothing(fun x -> x = 1);
        result.IsSome |> should be True