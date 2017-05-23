module AuthorizeByPermissionFilter

open System
open Xunit

[<Fact>]
let ``Authorization module`` () =
    let filter = AuthorizeByPermissionFilter("controller", "action")
    filter.