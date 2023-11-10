using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace MajesticRealtors.Pages
{
    public class HousesModel : PageModel
    {
        [BindProperty]
        public SelectList AreaList { get; set; }
        public string SearchArea { get; set; }
        public List<string> AllAreaList { get; set; }

        public void Onget(string query)
        {
            InitAreaDropDown();

            using (var webClient = new WebClient())
            {
                string Houses_data = string.Empty;
                try
                {
                    Houses_data = webClient.DownloadString("https://data.cityofchicago.org/resource/s6ha-ppgi.json");
                }
                // implemented specific exceptions related to web requests, for better error reporting instead of generic one.
                catch (WebException webEx)
                {
                    // Handle web request-related exceptions.
                    Console.WriteLine("WebException during API call - Housing: " + webEx.Message);
                    ViewData["UserHousesList"] = null;
                    return;
                }
                catch (Exception e)
                {
                    // Handle other exceptions.
                    Console.WriteLine("Exception during API call - Housing: " + e.Message);
                }
                var AllHousing = HouseData.Houses.FromJson(Houses_data);

                if (!string.IsNullOrWhiteSpace(query))
                {
                    var AllhousingList = AllHousing.ToList();
                    var CommunityHousing = AllhousingList.FindAll(x => string.Equals(x.CommunityArea, query, StringComparison.OrdinalIgnoreCase)).ToList();

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
