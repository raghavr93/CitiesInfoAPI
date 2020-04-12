using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    [ApiController]
    public class CitiesController : ControllerBase
    {
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<object>
                {
                    new {id = 1, Name = "Pune"},
                    new {id = 2, Name = "Mumbai"}
                }); 
        }
    }
}
