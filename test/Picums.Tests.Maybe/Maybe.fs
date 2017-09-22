module Picums.Tests.Maybe.Maybe

    open System
    open Picums.Maybe
    open Picums.Tests
    open Xunit
    open FsUnit.Xunit

    //[<Fact>]
    //let ``MaybeIsNeverNull`` () =
    //    let mutable possibleMaybe : Maybe<string> = Unchecked.defaultof<Maybe<string>>
    //    possibleMaybe |> should not' be null

    //[<Fact>]
    //let ``NullCastedMaybeIsNone`` () =
    //    Maybe<string> maybe = null;
    //    maybe.IsNone.Should().Be.True();

    //[<Fact>]
    //let ``NullCastedMaybeIsNotSome`` () =
    //    Maybe<string> maybe = null;
    //    maybe.IsSome.Should().Be.False();

    [<Fact>]
    let ``ValueCastedMaybeIsNotNone`` () =
        let maybe: Maybe<string> = Maybe.From("test");
        maybe.IsNone |> should be False;

    [<Fact>]
    let ``ValueCastedMaybeIsSome`` () =
        let maybe: Maybe<string> = Maybe.From("test");
        maybe.IsSome |> should be True;

    [<Theory>]
    [<InlineData(5, 3, 1)>]
    [<InlineData(5, 8, -1)>]
    [<InlineData(5, 5, 0)>]
    let ``ComparisionWorksForPayload`` (initialValue: int, compareToValue: int, result: int) =
        let maybe: Maybe<int> = Maybe.From(initialValue);
        let comparisionValue: int = maybe.CompareTo(compareToValue);
        result |> should equal comparisionValue;

    [<Theory>]
    [<InlineData(5, 3, 1)>]
    [<InlineData(5, 8, -1)>]
    [<InlineData(5, 5, 0)>]
    let ``ComparisionWorksForMaybes`` (initialValue: int, compareToValue: int, result: int) =
       let maybe = Maybe.From(initialValue);
       let other = Maybe.From(compareToValue);
       let comparisionValue = maybe.CompareTo(other);
       result |> should equal comparisionValue;

    [<Theory>]
    [<InlineData(5, 5, true)>]
    [<InlineData(5, 8, false)>]
    let ``EqualityWorksForPayload`` (initialValue: int, compareToValue: int, result: bool) =
       let maybe = Maybe.From(initialValue);
       let comparisionValue = maybe.Equals(compareToValue);
       result |> should equal comparisionValue;

    [<Theory>]
    [<InlineData(5, 5, true)>]
    [<InlineData(5, 8, false)>]
    let ``EqualityWorksForMaybes`` (initialValue: int, compareToValue: int, result: bool) =
       let maybe = Maybe.From(initialValue);
       let comparisionValue = maybe.Equals(compareToValue);
       result |> should equal comparisionValue;