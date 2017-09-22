module Picums.Tests.Maybe.Maybe

    open System
    open Picums.Maybe
    open Picums.Tests
    open Xunit
    open FsUnit.Xunit

    [<Fact>]
    let ``MaybeIsNeverNull`` () =
       let possibleMaybe: Maybe<string>  = Unchecked.defaultof<Maybe<string>>
       possibleMaybe |> should not' (be Null)

    [<Fact>]
    let ``NullCastedMaybeIsNone`` () =
       let maybe: Maybe<string>  = Unchecked.defaultof<Maybe<string>>
       maybe.IsNone |> should be True

    [<Fact>]
    let ``NullCastedMaybeIsNotSome`` () =
       let maybe: Maybe<string>  = Unchecked.defaultof<Maybe<string>>
       maybe.IsSome |> should be False

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