open Farmer
open FarmerExtension.MediaServices

let mediaServices =
    { Name = ResourceName "test"
      Location = Location.WestEurope
      StorageAccountId = "storageAccountId"
      StorageAccountType = Primary }

let deployment =
    arm {
        location Location.WestEurope
        add_resource mediaServices
    }

[<EntryPoint>]
let main argv =
    printf "Generating ARM template..."

    deployment
    |> Writer.quickWrite "generated-template"

    printfn "all done! Template written to generated-template.json"
    0 // return an integer exit code
