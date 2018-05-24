// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using face_shape_extractor.FacePlusPlus;
//
//    var data = Model.FromJson(jsonString);
//
using Newtonsoft.Json;
using System.Collections.Generic;

namespace fshapex.app.Services.FacePlusPlus
{
    public partial class Model
    {
        [JsonProperty("faces")]
        public List<Face> Faces { get; set; }

        [JsonProperty("image_id")]
        public string ImageId { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("time_used")]
        public long TimeUsed { get; set; }
    }

    public partial class Face
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("face_rectangle")]
        public FaceRectangle FaceRectangle { get; set; }

        [JsonProperty("face_token")]
        public string FaceToken { get; set; }

        [JsonProperty("landmark")]
        public Dictionary<string, Landmark> Landmark { get; set; }
    }

    public partial class Landmark
    {
        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }
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

    public partial class Attributes
    {
        [JsonProperty("age")]
        public Age Age { get; set; }

        [JsonProperty("beauty")]
        public Beauty Beauty { get; set; }

        [JsonProperty("ethnicity")]
        public Ethnicity Ethnicity { get; set; }

        [JsonProperty("gender")]
        public Ethnicity Gender { get; set; }

        [JsonProperty("skinstatus")]
        public Skinstatus Skinstatus { get; set; }
    }

    public partial class Skinstatus
    {
        [JsonProperty("acne")]
        public double Acne { get; set; }

        [JsonProperty("dark_circle")]
        public double DarkCircle { get; set; }

        [JsonProperty("health")]
        public double Health { get; set; }

        [JsonProperty("stain")]
        public double Stain { get; set; }
    }

    public partial class Ethnicity
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Beauty
    {
        [JsonProperty("female_score")]
        public double FemaleScore { get; set; }

        [JsonProperty("male_score")]
        public double MaleScore { get; set; }
    }

    public partial class Age
    {
        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public partial class Model
    {
        public static Model FromJson(string json) => JsonConvert.DeserializeObject<Model>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Model self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
