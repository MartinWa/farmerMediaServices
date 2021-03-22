module FarmerExtension.MediaServicesTransforms

open Farmer
open FarmerExtension.MediaServices
open Microsoft.Azure.Management.Media.Models

let mediaservicesTransforms =
    ResourceType("Microsoft.Media/mediaServices/transforms", "2018-07-01")

type MediaServicesTransformOnError =
    | StopProcessingJob
    | ContinueJob

type MediaServicesTransformRelativePriority =
    | Low
    | Normal
    | High

type MediaServicesTransformOutputs =
    { OnError: MediaServicesTransformOnError option
      RelativePriority: MediaServicesTransformRelativePriority option
      Preset: StandardEncoderPreset }

let outputParser (outputs: MediaServicesTransformOutputs) = ""

let propertyParser (description: string option) (outputs: seq<MediaServicesTransformOutputs>) =
    match description with
    | Some desc ->
        {| description = desc
           outputs = outputs |> Seq.map outputParser |} |> box
    | None -> {| outputs = outputs |> Seq.map outputParser |} |> box


type MediaServicesTransforms =
    { Name: ResourceName
      MediaServices: MediaServices
      Description: string option
      Outputs: seq<MediaServicesTransformOutputs> }
    interface IArmResource with
        member this.ResourceId =
            mediaservicesTransforms.resourceId this.Name

        member this.JsonModel =
            {| mediaservicesTransforms.Create(this.MediaServices.Name / this.Name, this.MediaServices.Location) with
                   properties = propertyParser this.Description this.Outputs |}
            :> _

    interface IBuilder with
        member this.ResourceId = mediaservices.resourceId this.Name

        member this.BuildResources location =
            [ { Name = this.Name
                MediaServices = this.MediaServices
                Description = this.Description
                Outputs = this.Outputs } ]


// {
//   "name": "string",
//   "type": "Microsoft.Media/mediaServices/transforms",
//   "apiVersion": "2018-07-01",
//   "properties": {
//     "description": "string",
//     "outputs": [
//       {
//         "onError": "string",
//         "relativePriority": "string",
//         "preset": {
//           "@odata.type": "string"
//         }
//       }
//     ]
//   },
//   "resources": []
// }
