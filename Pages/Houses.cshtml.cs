using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.IO;
using System;
using System.Linq.Expressions;

namespace MajesticRealtors.Pages
{
    public class HousesModel : PageModel
    {
        [BindProperty]
        public SelectList AreaList { get; set; }
        public string SearchArea { get; set; }
        public List<string> AllAreaList { get; set; }

        private readonly ILogger<HousesModel> _logger;

        public HousesModel(ILogger<HousesModel> logger)
        {
            _logger = logger;
        }

        public void Onget(string query)
        {
            InitAreaDropDown();

            //Get the data
            using (var webClient = new WebClient())
            {
                string Houses_data = string.Empty;
                try
                {
                    Houses_data = webClient.DownloadString("https://data.cityofchicago.org/resource/s6ha-ppgi.json?community_area="+query);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error during API call - Housing", e);

                }
                var AllHousing = HouseData.Houses.FromJson(Houses_data);

                if (!string.IsNullOrWhiteSpace(query))
                {
                    var CommunityHousing = AllHousing.ToList();
                    ViewData["UserHousesList"] = CommunityHousing;
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

            //Read the neighborhood list from the text file.
            try
            {
                string[] AllAreaList = System.IO.File.ReadAllLines("neighborhood.txt");
                ViewData["SearchArea"] = new SelectList(AllAreaList);
            }
            catch(Exception ex){
                LogException(ex);
            }

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
