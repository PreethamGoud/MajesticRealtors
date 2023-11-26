using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.IO;
using System;
using System.Linq.Expressions;
using AllPropertiesData;
using HouseData;
using LandData;
using Microsoft.Extensions.Configuration;

namespace MajesticRealtors.Pages
{
    public class AllPropertiesModel : PageModel
    {
        [BindProperty]
        public SelectList AreaList { get; set; }
        public string SearchArea { get; set; }
        public List<string> AllAreaList { get; set; }

        private readonly ILogger<AllPropertiesModel> _logger;

        public AllPropertiesModel(ILogger<AllPropertiesModel> logger)
        {
            _logger = logger;
        }

        public void Onget(string query)
        {
            InitAreaDropDown();

            using (var webClient = new WebClient())
            {
                string Houses_data = string.Empty;
                try
                {
                    Houses_data = webClient.DownloadString("https://data.cityofchicago.org/resource/s6ha-ppgi.json?community_area=" + query);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error during API call - Housing", e);

                }
                var AllHousing = HouseData.Houses.FromJson(Houses_data);

                string Lands_data = string.Empty;
                try
                {
                    Lands_data = webClient.DownloadString("https://data.cityofchicago.org/resource/aksk-kvfp.json");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error during API call - Land Inventory", e);

                }


                var AllLands = Lands.FromJson(Lands_data);

                if (!string.IsNullOrWhiteSpace(query))
                {
                    var AllhousingList = AllHousing.ToList();
                    var CommunityHousing = AllhousingList.FindAll(x => string.Equals(x.CommunityArea, query, StringComparison.OrdinalIgnoreCase)).ToList();

                    var AlllandsList = AllLands.ToList();
                    var CommunityLands = AlllandsList.FindAll(x => string.Equals(x.CommunityAreaName, query, StringComparison.OrdinalIgnoreCase)).ToList();

                    List<AllProperties> CommunityProperties = new List<AllProperties>();

                    foreach (var CommunityHouse in CommunityHousing)
                    {
                        CommunityProperties.Add(new AllProperties
                        {
                            CommunityArea = CommunityHouse.CommunityArea,
                            CommunityAreaNumber = CommunityHouse.CommunityAreaNumber,
                            PropertyType = "House",
                            PropertyName = CommunityHouse.PropertyName,
                            Address = CommunityHouse.Address,
                            ZipCode = CommunityHouse.ZipCode,
                            ManagementCompany = CommunityHouse.ManagementCompany,
                            Latitude = CommunityHouse.Latitude,
                            Longitude = CommunityHouse.Longitude
                        });
                    }

                    foreach (var CommunityLand in CommunityLands)
                    {
                        CommunityProperties.Add(new AllProperties
                        {
                            CommunityArea = CommunityLand.CommunityAreaName,
                            CommunityAreaNumber = CommunityLand.CommunityAreaNumber,
                            PropertyType = "Land",
                            PropertyName = CommunityLand.ManagingOrganization,
                            Address = CommunityLand.Address,
                            ZipCode = CommunityLand.ZipCode,
                            ManagementCompany = CommunityLand.ManagingOrganization,
                            Latitude = CommunityLand.Latitude,
                            Longitude = CommunityLand.Longitude
                        });
                    }

                    if (CommunityProperties != null && CommunityProperties.Count > 0)
                    {

                        ViewData["UserPropertiesList"] = CommunityProperties;
                    }
                    else
                    {
                        ViewData["UserPropertiesList"] = null;
                    }
                }
                else
                {
                    ViewData["UserPropertiesList"] = null;
                }
                SearchArea = query;
            }
        }

        private void InitAreaDropDown()
        {
            AllAreaList = new List<string>
            {
                {"Englewood" },
                {"Edgewater"} ,
                {"Roseland"} ,
                {"Humboldt Park"} ,
                {"Grand Boulevard"} ,
                {"Woodlawn"} ,
                {"Oakland"} ,
                {"West Englewood"} ,
                {"Near North Side"} ,
                {"West Town"} ,
                {"Montclare"} ,
                {"Albany Park"} ,
                {"Grand Boulevard"} ,
                {"Woodlawn"} ,
                {"Portage Park"} ,
                {"Washington Park"} ,
                {"Lake View"} ,
                {"Greater Grand Crossing"} ,
                {"Loop"} ,
                {"Near West Side"} ,
                {"North Lawndale"} ,
                {"Auburn Gresham"} ,
                {"West Town"} ,
                {"Humboldt Park"} ,
                {"Near North Side"} ,
                {"South Shore"} ,
                {"West Town"} ,
                {"Near West Side"} ,
                {"Logan Square"} ,
                {"Austin"} ,
                {"Uptown"} ,
                {"East Garfield Park"} ,
                {"Humboldt Park"} ,
                {"Woodlawn"} ,
                {"Oakland"} ,
                {"Humboldt Park"} ,
                {"Lake View"} ,
                {"Lincoln Square"} ,
                {"Douglas"} ,
                {"Washington Park"} ,
                {"Belmont Cragin"} ,
                {"New City"} ,
                {"North Lawndale"} ,
                {"West Ridge"} ,
                {"Chicago Lawn"} ,
                {"Belmont Cragin"} ,
                {"Near South Side"} ,
                {"Uptown"} ,
                {"Douglas"} ,
                {"Austin"} ,
                {"Lincoln Square"} ,
                {"South Deering"} ,
                {"Grand Boulevard"} ,
                {"Logan Square"} ,
                {"West Town"} ,
                {"Hyde Park"} ,
                {"Logan Square"} ,
                {"Austin"} ,
                {"Washington Park"} ,
                {"Douglas"} ,
                {"Grand Boulevard"} ,
                {"Near West Side"} ,
                {"Washington Park"} ,
                {"North Lawndale"} ,
                {"Lower West Side"} ,
                {"New City"} ,
                {"West Town"} ,
                {"East Garfield Park"} ,
                {"North Lawndale"} ,
                {"Englewood"} ,
                {"Near South Side"} ,
            };

            ViewData["SearchArea"] = new SelectList(AllAreaList);
        }
        private void LogException(Exception ex)
        {
            try
            {
                // Log the exception to a text file
                using (StreamWriter writer = new StreamWriter("error_log.txt", true))
                {
                    writer.WriteLine($"[{DateTime.Now}] {ex.GetType().Name}: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine(new string('-', 50));
                }
            }
            catch
            {
                // If logging fails, just ignore it to avoid infinite loop of exceptions
            }
        }

    }
}
