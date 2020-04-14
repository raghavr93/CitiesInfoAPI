using CityInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>() {

                 new CityDto
                 {
                     Id = 1,
                     Name = "Pune",
                     Description = "Most Livable City in India",
                     PointOfInterest = new List<PointOfInterestDto>()
                     {
                       new PointOfInterestDto
                       {
                           Id = 1,
                           Name = "Koregaon Park",
                           Description = "Party Capital"
                       },
                       new PointOfInterestDto
                       {
                           Id = 2,
                           Name = "Magarpatta",
                           Description = "IT Hub"
                       }
                     }
                 },
                 new CityDto
                 {
                     Id = 2,
                     Name = "Mumbai",
                     Description = "Business Capital of India",
                     PointOfInterest = new List<PointOfInterestDto>()
                     {
                       new PointOfInterestDto
                       {
                           Id = 1,
                           Name = "Colaba",
                           Description = "High Class Society"
                       },
                       new PointOfInterestDto
                       {
                           Id = 2,
                           Name = "Santa Cruz",
                           Description = "Where the Airport at"
                       }
                     }
                 }

            };
        }
    }
}
