using AllPropertiesData;
using LandData;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MajesticRealtors.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MRController : ControllerBase
    {
        // GET: api/<MRController>
        [HttpGet]
        public IActionResult Get()
        {
            List<AllProperties> CommunityProperties = new List<AllProperties>();
            using (var webClient = new WebClient())
            {
                string Houses_data = string.Empty;
                try
                {
                    Houses_data = webClient.DownloadString("https://data.cityofchicago.org/resource/s6ha-ppgi.json");
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

                
                    var AllhousingList = AllHousing.ToList();

                    var AlllandsList = AllLands.ToList();

               

                    foreach (var CommunityHouse in AllhousingList)
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

                    foreach (var CommunityLand in AlllandsList)
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




                
            }
            return Ok(CommunityProperties);
        }

        // GET api/<MRController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MRController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MRController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MRController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
