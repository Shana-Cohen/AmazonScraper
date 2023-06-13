using AmazonScraperDemo.Scraping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonScraperDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmazonController : ControllerBase
    {
        [HttpGet]
        [Route("scrape/{searchText}")]
        public List<AmazonItem> Scrape(string searchText)
        {
            return AmazonScraper.Scrape(searchText);
        }
    }
}
