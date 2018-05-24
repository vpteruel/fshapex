// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using face_shape_extractor.Kairos;
//
//    var data = Model.FromJson(jsonString);
//
using Newtonsoft.Json;
using System.Collections.Generic;

namespace fshapex.app.Services.Kairos
{
    public partial class Model
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("media_info")]
        public MediaInfo MediaInfo { get; set; }

        [JsonProperty("frames")]
        public List<Frame> Frames { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("status_code")]
        public long StatusCode { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }
    }

    public partial class MediaInfo
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Frame
    {
        [JsonProperty("people")]
        public List<Person> People { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }

    public partial class Person
    {
        [JsonProperty("emotions")]
        public Emotions Emotions { get; set; }

        [JsonProperty("demographics")]
        public Demographics Demographics { get; set; }

        [JsonProperty("appearance")]
        public Appearance Appearance { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("landmarks")]
        public List<Dictionary<string, Landmark>> Landmarks { get; set; }

        [JsonProperty("pose")]
        public Pose Pose { get; set; }

        [JsonProperty("face")]
        public Face Face { get; set; }

        [JsonProperty("person_id")]
        public long PersonId { get; set; }

        [JsonProperty("tracking")]
        public Tracking Tracking { get; set; }
    }

    public partial class Emotions
    {
        [JsonProperty("disgust")]
        public long Disgust { get; set; }

        [JsonProperty("joy")]
        public long Joy { get; set; }

        [JsonProperty("anger")]
        public long Anger { get; set; }

        [JsonProperty("fear")]
        public long Fear { get; set; }

        [JsonProperty("sadness")]
        public long Sadness { get; set; }

        [JsonProperty("surprise")]
        public long Surprise { get; set; }
    }

    public partial class Demographics
    {
        [JsonProperty("age_group")]
        public string AgeGroup { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }

    public partial class Appearance
    {
        [JsonProperty("glasses")]
        public string Glasses { get; set; }
    }

    public partial class Landmark
    {
        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }
    }

    public partial class Pose
    {
        [JsonProperty("roll")]
        public double Roll { get; set; }

        [JsonProperty("pitch")]
        public double Pitch { get; set; }

        [JsonProperty("yaw")]
        public double Yaw { get; set; }
    }

    public partial class Face
    {
        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }
    }

    public partial class Tracking
    {
        [JsonProperty("blinking")]
        public string Blinking { get; set; }

        [JsonProperty("attention")]
        public long Attention { get; set; }

        [JsonProperty("dwell")]
        public long Dwell { get; set; }

        [JsonProperty("glances")]
        public long Glances { get; set; }
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
