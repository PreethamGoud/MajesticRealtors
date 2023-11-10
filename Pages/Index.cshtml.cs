using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LandData;
using System.Net;
using HouseData;

namespace MajesticRealtors.Pages
{

    public class IndexModel : PageModel
    {
        static readonly HttpClient httpClient = new HttpClient();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var houseJSON = await httpClient.GetStringAsync("https://data.cityofchicago.org/resource/s6ha-ppgi.json");
                var houseCollection = Houses.FromJson(houseJSON);
                ViewData["Houses"] = houseCollection;

                var landJSON = await httpClient.GetStringAsync("https://data.cityofchicago.org/resource/aksk-kvfp.json");
                var landCollection = Lands.FromJson(landJSON);
                ViewData["Lands"] = landCollection;
            }
        }
    }
}