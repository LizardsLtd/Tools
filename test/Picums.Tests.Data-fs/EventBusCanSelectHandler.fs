module Picums.Tests.Data.EventBusCanSelectHandler

    open System
    open Picums.Data.Events
    open Picums.Tests
    open Should.Fluent
    open Xunit

    let logger = TestLoggerFactory()
    let handlers =  List.empty<IEventHandler>
    let eventBus  = new EventBus(handlers)

    type TestEvent(id: Guid) =
        interface IEvent with
            member this.EventId = id

    [<Fact>]
    let ``EventBus can execute the Event`` () =
        let event = new TestEvent(Guid.NewGuid())
        eventBus.Publish(event)