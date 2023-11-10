using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace MajesticRealtors.Pages
{
    public class LandsModel : PageModel
    {
        [BindProperty]
        public string CommunityNumberItem { get; set; }

        public async Task OnGetAsync(string query)
        {
            using (var webClient = new WebClient())
            {
                string Lands_data = string.Empty;
                try
                {
                    Lands_data = webClient.DownloadString("https://data.cityofchicago.org/resource/aksk-kvfp.json");
                }
                // implemented specific exceptions related to web requests, for better error reporting instead of generic one.
                catch (WebException webEx)
                {
                    // Handle web request-related exceptions.
                    Console.WriteLine("WebException during API call - Land Inventory: " + webEx.Message);
                    ViewData["UserLandsList"] = null;
                    return; 
                }
                catch (Exception e)
                {
                    // Handle other exceptions.
                    Console.WriteLine("Exception during API call - Land Inventory: " + e.Message);
                    ViewData["UserLandsList"] = null;
                    return; 
                }
                var AllLandInventory = LandData.Lands.FromJson(Lands_data);

                if (!string.IsNullOrWhiteSpace(query))
                {
                    var AllLandsList = AllLandInventory.ToList();
                    long UserCommunityNumber = (long)Convert.ToDouble(query);
                    // Instead of multiple calls to FindAll and modifying the same list in place, used LINQ to filter the data in a more readable and functional way
                    var CommunityLand = AllLandsList
                     .Where(x => x.CommunityAreaNumber == UserCommunityNumber && x.SqFt >= 0)
                     .OrderByDescending(x => x.SqFt)
                     .ToList();
                    if (CommunityLand != null && CommunityLand.Count > 0)
                    {
                        ViewData["UserLandsList"] = CommunityLand;
                    }
                    else
                    {
                        ViewData["UserLandsList"] = null;
                    }
                }
                else
                {
                    ViewData["UserLandsList"] = null;
                }
                CommunityNumberItem = query;
            }
        }


    }
}
