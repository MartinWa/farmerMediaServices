open Farmer
open Farmer.Builders.Storage
open FarmerExtension.MediaServices
open FarmerExtension.MediaServicesTransforms

let storage =
    storageAccount { name "teststorageaccount" }

let mediaServices =
    { Name = ResourceName "test"
      Location = Location.WestEurope
      PrimaryStorageAccount = storage
      SecondaryStorageAccount = None }

let mediaServicesTransforms =
    { Name = ResourceName "test2"
      MediaServices = mediaServices
      Description = None
      Outputs = [] }

let deployment =
    arm {
        location Location.WestEurope

        add_resources [ storage
                        mediaServices
                        mediaServicesTransforms ]
    }

[<EntryPoint>]
let main argv =
    printf "Generating ARM template..."

    deployment
    |> Writer.quickWrite "generated-template"

    printfn "all done! Template written to generated-template.json"
    0 // return an integer exit code
