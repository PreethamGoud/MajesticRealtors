﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LandData;
using System.Net;
using HouseData;

namespace MajesticRealtors.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {


            using (var webClient = new WebClient())
            {

                var houseJSON = webClient.DownloadString("https://data.cityofchicago.org/resource/s6ha-ppgi.json");
                List<Houses> houseCollection = Houses.FromJson(houseJSON);

                ViewData["Houses"] = houseCollection;

                var landJSON = webClient.DownloadString("https://data.cityofchicago.org/resource/aksk-kvfp.json");
                List<Lands> landCollection = Lands.FromJson(landJSON);


                ViewData["Lands"] = landCollection;

            }


        }
    }
}