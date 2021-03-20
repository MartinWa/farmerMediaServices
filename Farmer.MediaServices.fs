module FarmerExtension.MediaServices

open Farmer

let mediaservices =
    ResourceType("Microsoft.Media/mediaservices", "2018-07-01")

type StorageAccountType =
    | Primary
    | Secondary

type Mediaservices =
    { Name: ResourceName
      Location: Location
      StorageAccountId: ResourceId // TODO Change to a list of storage accounts
      StorageAccountType: StorageAccountType }
    interface IArmResource with
        member this.ResourceId = mediaservices.resourceId this.Name

        member this.JsonModel =
            {| mediaservices.Create(this.Name, this.Location) with
                   properties =
                       {| storageAccounts =
                              [| {| id = this.StorageAccountId.ToString  //TODO This is not the ID we are looking for
                                    ``type`` = this.StorageAccountType.ToString() |} |] |} |}
            :> _

    interface IBuilder with
        member this.ResourceId = mediaservices.resourceId this.Name

        member this.BuildResources location =
            [ { Name = this.Name
                Location = location
                StorageAccountId = this.StorageAccountId
                StorageAccountType = this.StorageAccountType } ]
