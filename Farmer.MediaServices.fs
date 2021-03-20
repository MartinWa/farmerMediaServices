module FarmerExtension.MediaServices

open Farmer

let mediaservices =
    ResourceType("Microsoft.Media/mediaservices", "2018-07-01")

type Mediaservices =
    { Name: ResourceName
      Location: Location
      StorageAccountId: string //TODO This should be a Farmer type from storage
      StorageAccountType: string } // TODO Enum
    interface IArmResource with
        member this.ResourceId = mediaservices.resourceId this.Name

        member this.JsonModel =
            {| mediaservices.Create(this.Name, this.Location) with
                   properties =
                       {| storageAccounts =
                              [| {| id = this.StorageAccountId
                                    ``type`` = this.StorageAccountType |} |] |} |}
            :> _
