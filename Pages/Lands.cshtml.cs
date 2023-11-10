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
        private readonly ILogger<HousesModel> _logger;
        private const string LandApiUrl = "https://data.cityofchicago.org/resource/aksk-kvfp.json";
        [BindProperty]
        public string CommunityNumberItem { get; set; }


        public async Task OnGetAsync(string query)
        {
            try
            {
                using (var webClient = new WebClient())
            {
                string Lands_data = string.Empty;
                try
                {
                    Lands_data = webClient.DownloadString(LandApiUrl);
                }
                catch (Exception e)
                {
                    throw new Exception("Error during API call - Land Inventory", e);
                }
                var AllLandInventory = LandData.Lands.FromJson(Lands_data);

                if (!string.IsNullOrWhiteSpace(query) && double.TryParse(query, out double convertedValue))
                {
                    var AllLandsList = AllLandInventory.ToList();
                    long UserCommunityNumber = (long)convertedValue;
                    var CommunityLand = AllLandsList.FindAll(x => (x.CommunityAreaNumber == UserCommunityNumber));
                    CommunityLand = CommunityLand.FindAll(x => x.SqFt >= 0);
                    CommunityLand = CommunityLand.OrderByDescending(x => x.SqFt).ToList();
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
            catch (Exception e)
            {
                _logger.LogError("Error during API call - Housing", e);
            }
        }

    }
}
