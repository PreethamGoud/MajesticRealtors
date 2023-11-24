using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MajesticRealtors.Pages
{
    public class SurveyModel : PageModel
    {
        private readonly ILogger<SurveyModel> _logger;

        [BindProperty]
        public DreamHouses House { get; set; }

        public SurveyModel(ILogger<SurveyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["DreamHouseList"] = AllDreamHouses.AllHouses;
        }

        public void OnPost()
        {

            string stuff = House.CommunityArea + House.PropertyType + House.Street + House.PlotSize
                + House.ManagementCompany;
            AllDreamHouses.AllHouses.Add(House);

            ViewData["DreamHouseList"] = AllDreamHouses.AllHouses;
        }
    }
}
