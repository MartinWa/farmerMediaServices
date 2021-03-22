open Farmer
open Farmer.Builders.Storage
open FarmerExtension.MediaServices

let storage =
    storageAccount { name "teststorageaccount" }

let mediaServices =
    { Name = ResourceName "test"
      Location = Location.WestEurope
      PrimaryStorageAccount = storage
      SecondaryStorageAccount = None }

let deployment =
    arm {
        location Location.WestEurope
        add_resources [ storage; mediaServices ]
    }


// let preset = new StandardEncoderPreset(new List<Codec> { new Codec() }, new List<Format> { new Format() }, new Filters())

// let json = JsonSerializer.Serialize preset

[<EntryPoint>]
let main argv =
    printf "Generating ARM template..."

    deployment
    |> Writer.quickWrite "generated-template"

    printfn "all done! Template written to generated-template.json"

    // printfn "%A" json


    0 // return an integer exit code
