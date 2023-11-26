using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace MajesticRealtors.Pages
{
    public class SalariesModel : PageModel
    {
        [BindProperty]
        public SelectList AreaList { get; set; }
        public string SearchArea { get; set; }
        public List<string> AllAreaList { get; set; }

        private readonly ILogger<SalariesModel> _logger;

        public SalariesModel(ILogger<SalariesModel> logger)
        {
            _logger = logger;
        }

        public void Onget(string query)
        {
            InitAreaDropDown();

            //Get the data
            using (var webClient = new WebClient())
            {
                string Salaries_Data = string.Empty;
                try
                {
                    Salaries_Data = webClient.DownloadString("https://cityofcincy.azurewebsites.net/Data/GetData");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error during API call - Salaries", e);

                }
                var AllSalaries = Salaries.FromJson(Salaries_Data);

                if (!string.IsNullOrWhiteSpace(query))
                {
                    var AllSalariesList = AllSalaries.ToList();
                    var SalaryFilter = AllSalariesList.FindAll(x => string.Equals(x.DepartmentName, query, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (SalaryFilter != null && SalaryFilter.Count > 0)
                    {

                        ViewData["UserSalaryList"] = SalaryFilter;
                    }
                    else
                    {
                        ViewData["UserSalaryList"] = null;
                    }
                }
                else
                {
                    ViewData["UserSalaryList"] = null;
                }
                SearchArea = query;
            }
        }

        private void InitAreaDropDown()
        {

            //Read the neighborhood list from the text file.
            try
            {
                string[] AllAreaList = System.IO.File.ReadAllLines("departments.txt");
                ViewData["SearchArea"] = new SelectList(AllAreaList);
            }
            catch (Exception ex)
            {
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
