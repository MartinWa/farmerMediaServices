open Farmer
open Farmer.Builders.Storage
open FarmerExtension.MediaServices

let storage =
    storageAccount { name "teststorageaccount" }

let mediaServices =
    { Name = ResourceName "test"
      Location = Location.WestEurope
      StorageAccountId = storage.ResourceId
      StorageAccountType = Primary }

let deployment =
    arm {
        location Location.WestEurope
        add_resources [ storage; mediaServices ]
    }

[<EntryPoint>]
let main argv =
    printf "Generating ARM template..."

    deployment
    |> Writer.quickWrite "generated-template"

    printfn "all done! Template written to generated-template.json"
    0 // return an integer exit code
