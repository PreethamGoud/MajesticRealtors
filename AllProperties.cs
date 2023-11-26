using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Globalization;

namespace AllPropertiesData
{
    public partial class AllProperties
    {
        [JsonProperty("community_area")] 
        public string CommunityArea { get; set; }

        [JsonProperty("community_area_number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CommunityAreaNumber { get; set; }

        [JsonProperty("property_type")]
        public string PropertyType { get; set; }

        [JsonProperty("property_name")]
        public string PropertyName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("zip_code")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ZipCode { get; set; }

        [JsonProperty("management_company")]
        public string ManagementCompany { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public string Latitude { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public string Longitude { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public Location Location { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("human_address")]
        public string HumanAddress { get; set; }
    }

    

        public partial class AllProperties
        {
            public static List<AllProperties> FromJson(string json) => JsonConvert.DeserializeObject<List<AllProperties>>(json, AllPropertiesData.Converter.Settings);
        }
        public static class Serialize
        {
            public static string ToJson(this List<AllProperties> self) => JsonConvert.SerializeObject(self, AllPropertiesData.Converter.Settings);
        }
        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }
        internal class ParseStringConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                long l;
                if (Int64.TryParse(value, out l))
                {
                    return l;
                }
                throw new Exception("Cannot unmarshal type long");
            }
            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (long)untypedValue;
                serializer.Serialize(writer, value.ToString());
                return;
            }
            public static readonly ParseStringConverter Singleton = new ParseStringConverter();
        }
}

