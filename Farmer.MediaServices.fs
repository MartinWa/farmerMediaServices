module FarmerExtension.MediaServices

open Farmer
open Farmer.Builders

let mediaservices =
    ResourceType("Microsoft.Media/mediaservices", "2018-07-01")

let storageAccountParser (primary: StorageAccountConfig) (secondary: StorageAccountConfig option) =
    match secondary with
    | Some account ->
        [ primary, "Primary"
          account, "Secondary" ]
    | None -> [ primary, "Primary" ]
    |> Seq.map
        (fun (account, typ) ->
            {| id = account.ResourceId.ArmExpression.Value
               ``type`` = typ |})

type Mediaservices =
    { Name: ResourceName
      Location: Location
      PrimaryStorageAccount: StorageAccountConfig
      SecondaryStorageAccount: StorageAccountConfig option }

    interface IArmResource with
        member this.ResourceId = mediaservices.resourceId this.Name

        member this.JsonModel =
            {| mediaservices.Create(this.Name, this.Location) with
                   properties =
                       {| storageAccounts = storageAccountParser this.PrimaryStorageAccount this.SecondaryStorageAccount |} |}
            :> _

    interface IBuilder with
        member this.ResourceId = mediaservices.resourceId this.Name

        member this.BuildResources location =
            [ { Name = this.Name
                Location = location
                PrimaryStorageAccount = this.PrimaryStorageAccount
                SecondaryStorageAccount = this.SecondaryStorageAccount } ]
