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
}
