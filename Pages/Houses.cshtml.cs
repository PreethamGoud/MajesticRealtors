using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace MajesticRealtors.Pages
{
    public class HousesModel : PageModel
    {
        private readonly ILogger<HousesModel> _logger;
        [BindProperty]
        public SelectList AreaList { get; set; }
        public string SearchArea { get; set; }
        public List<string> AllAreaList { get; set; }
        private const string HousingApiUrl = "https://data.cityofchicago.org/resource/s6ha-ppgi.json";


        public void Onget(string query)
        {
            InitAreaDropDown();

            try
            {

                using (var webClient = new WebClient())
                {
                    string Houses_data = string.Empty;
                    try
                    {
                        Houses_data = webClient.DownloadString(HousingApiUrl);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error during API call - Housing", e);

                    }
                    var AllHousing = HouseData.Houses.FromJson(Houses_data);

                    if (!string.IsNullOrWhiteSpace(query))
                    {
                        var AllhousingList = AllHousing.ToList();
                        var CommunityHousing = AllhousingList.Where(x => string.Equals(x.CommunityArea, query, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (CommunityHousing != null && CommunityHousing.Count > 0)
                        {

                            ViewData["UserHousesList"] = CommunityHousing;
                        }
                        else
                        {
                            ViewData["UserHousesList"] = null;
                        }
                    }
                    else
                    {
                        ViewData["UserHousesList"] = null;
                    }
                    SearchArea = query;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error during API call - Housing", e);
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

    }
}

