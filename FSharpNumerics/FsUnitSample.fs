module FSharpNumerics.Tests

open Xunit
open FsUnit.Xunit

[<Fact>]  
let ``when I ask whether it is On it answers true.``()=
    true |> should be True

[<Fact>] 
let ``when I convert it to a string it becomes "On".``()=
    "On" |> should equal "On"