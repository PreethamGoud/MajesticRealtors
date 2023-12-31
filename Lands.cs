﻿namespace LandData
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Lands
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("pin")]
        public string Pin { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("managing_organization")]
        public string ManagingOrganization { get; set; }

        [JsonProperty("property_status")]
        public string PropertyStatus { get; set; }

        [JsonProperty("sq_ft")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SqFt { get; set; }

        [JsonProperty("ward")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Ward { get; set; }

        [JsonProperty("community_area_number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CommunityAreaNumber { get; set; }

        [JsonProperty("community_area_name")]
        public string CommunityAreaName { get; set; }

        [JsonProperty("zoning_classification")]
        public string ZoningClassification { get; set; }

        [JsonProperty("zip_code")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ZipCode { get; set; }

        [JsonProperty("last_update")]
        public string LastUpdate { get; set; }

        [JsonProperty("x_coordinate")]
        public string XCoordinate { get; set; }

        [JsonProperty("y_coordinate")]
        public string YCoordinate { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty(":@computed_region_rpca_8um6")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ComputedRegionRpca8Um6 { get; set; }

        [JsonProperty(":@computed_region_vrxf_vc4k")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ComputedRegionVrxfVc4K { get; set; }

        [JsonProperty(":@computed_region_6mkv_f3dw")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ComputedRegion6MkvF3Dw { get; set; }

        [JsonProperty(":@computed_region_bdys_3d7i")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ComputedRegionBdys3D7I { get; set; }

        [JsonProperty(":@computed_region_43wa_7qmu")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ComputedRegion43Wa7Qmu { get; set; }

        [JsonProperty(":@computed_region_awaf_s7ux")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ComputedRegionAwafS7Ux { get; set; }
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

    public partial class Lands
    {
        public static List<Lands> FromJson(string json) => JsonConvert.DeserializeObject<List<Lands>>(json, LandData.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Lands> self) => JsonConvert.SerializeObject(self, LandData.Converter.Settings);
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
