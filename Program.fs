open Farmer
open Farmer.Builders

let storage =
    storageAccount {
        name "test"
        sku Storage.Sku.Standard_GRS
    }

let deployment = arm { add_resources [ storage ] }

[<EntryPoint>]
let main argv =
    printf "Generating ARM template..."
    deployment |> Writer.quickWrite "generated-template"
    printfn "all done! Template written to generated-template.json"
    0 // return an integer exit code
