// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using face_shape_extractor.CognitiveServices;
//
//    var data = Model.FromJson(jsonString);
//
using Newtonsoft.Json;
using System.Collections.Generic;

namespace fshapex.app.Services.CognitiveServices
{
    public partial class Model
    {
        [JsonProperty("faceId")]
        public string FaceId { get; set; }

        [JsonProperty("faceLandmarks")]
        public FaceLandmarks FaceLandmarks { get; set; }

        [JsonProperty("faceRectangle")]
        public FaceRectangle FaceRectangle { get; set; }
    }

    public partial class FaceRectangle
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("left")]
        public long Left { get; set; }

        [JsonProperty("top")]
        public long Top { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class FaceLandmarks
    {
        [JsonProperty("eyeLeftBottom")]
        public Landmark EyeLeftBottom { get; set; }

        [JsonProperty("eyeLeftInner")]
        public Landmark EyeLeftInner { get; set; }

        [JsonProperty("eyeLeftOuter")]
        public Landmark EyeLeftOuter { get; set; }

        [JsonProperty("eyeLeftTop")]
        public Landmark EyeLeftTop { get; set; }

        [JsonProperty("eyeRightBottom")]
        public Landmark EyeRightBottom { get; set; }

        [JsonProperty("eyeRightInner")]
        public Landmark EyeRightInner { get; set; }

        [JsonProperty("eyeRightOuter")]
        public Landmark EyeRightOuter { get; set; }

        [JsonProperty("eyeRightTop")]
        public Landmark EyeRightTop { get; set; }

        [JsonProperty("eyebrowLeftInner")]
        public Landmark EyebrowLeftInner { get; set; }

        [JsonProperty("eyebrowLeftOuter")]
        public Landmark EyebrowLeftOuter { get; set; }

        [JsonProperty("eyebrowRightInner")]
        public Landmark EyebrowRightInner { get; set; }

        [JsonProperty("eyebrowRightOuter")]
        public Landmark EyebrowRightOuter { get; set; }

        [JsonProperty("mouthLeft")]
        public Landmark MouthLeft { get; set; }

        [JsonProperty("mouthRight")]
        public Landmark MouthRight { get; set; }

        [JsonProperty("noseLeftAlarOutTip")]
        public Landmark NoseLeftAlarOutTip { get; set; }

        [JsonProperty("noseLeftAlarTop")]
        public Landmark NoseLeftAlarTop { get; set; }

        [JsonProperty("noseRightAlarOutTip")]
        public Landmark NoseRightAlarOutTip { get; set; }

        [JsonProperty("noseRightAlarTop")]
        public Landmark NoseRightAlarTop { get; set; }

        [JsonProperty("noseRootLeft")]
        public Landmark NoseRootLeft { get; set; }

        [JsonProperty("noseRootRight")]
        public Landmark NoseRootRight { get; set; }

        [JsonProperty("noseTip")]
        public Landmark NoseTip { get; set; }

        [JsonProperty("pupilLeft")]
        public Landmark PupilLeft { get; set; }

        [JsonProperty("pupilRight")]
        public Landmark PupilRight { get; set; }

        [JsonProperty("underLipBottom")]
        public Landmark UnderLipBottom { get; set; }

        [JsonProperty("underLipTop")]
        public Landmark UnderLipTop { get; set; }

        [JsonProperty("upperLipBottom")]
        public Landmark UpperLipBottom { get; set; }

        [JsonProperty("upperLipTop")]
        public Landmark UpperLipTop { get; set; }
    }

    public partial class Landmark
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }

    public partial class Model
    {
        public static List<Model> FromJson(string json) => JsonConvert.DeserializeObject<List<Model>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Model> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
