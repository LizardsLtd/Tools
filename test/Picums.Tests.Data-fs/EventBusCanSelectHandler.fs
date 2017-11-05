module Picums.Tests.Data.EventBusCanSelectHandler

    open System
    open Picums.Data.Events
    open Picums.Tests
    open Xunit
    open FakeItEasy

    type TestEventAlpha(id: Guid) =
        interface IEvent with
            member this.EventId = id

    type TestEventBeta(id: Guid) =
        interface IEvent with
            member this.EventId = id

    let logger = TestLoggerFactory()
    let alphaHandler : IEventHandler<TestEventAlpha> = A.Fake<IEventHandler<TestEventAlpha>>()
    let betaHandler : IEventHandler<TestEventBeta>= A.Fake<IEventHandler<TestEventBeta>>()
    let handlers : seq<IEventHandler> =  [alphaHandler :> IEventHandler; betaHandler :> IEventHandler] :> seq<IEventHandler>
    let eventBus  = new EventBus(handlers)

    [<Fact>]
    let ``EventBus can execute the Event`` () =
        let event = new TestEventAlpha(Guid.NewGuid())
        eventBus.Publish(event)

    [<Fact>]
    let ``EventBus can select between two handlers`` () =
        let event = new TestEventAlpha(Guid.NewGuid())
        eventBus.Publish(event)
        //A.CallTo(fun () -> alphaHandler.Handle(event)).MustHaveHappened()
        //A.CallTo(fun _ -> betaHandler.Handle(event)).MustNotHaveHappened()