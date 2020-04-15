using CityInfo.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{

    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{id}", Name = "GetPointofInterest")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointofinterest = city.PointOfInterest.FirstOrDefault(c => c.Id == id);

            if (pointofinterest == null)
            {
                return NotFound();
            }

            return Ok(pointofinterest);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, 
                                 [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can't be same.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfINterest = city.PointOfInterest.Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfINterest,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, 
                                  [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can't be same.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFormStore = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFormStore == null)
            {
                return NotFound();
            }

            pointOfInterestFormStore.Name = pointOfInterest.Name;
            pointOfInterestFormStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
                                  [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFormStore = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFormStore == null)
            {
                return NotFound();
            }

            var finalPointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFormStore.Name,
                Description = pointOfInterestFormStore.Description
            };

            patchDoc.ApplyTo(finalPointOfInterestToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFormStore.Name = finalPointOfInterestToPatch.Name;
            pointOfInterestFormStore.Description = finalPointOfInterestToPatch.Description;

            if (finalPointOfInterestToPatch.Name == finalPointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can't be same.");
            }

            if(!TryValidateModel(finalPointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId,int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFormStore = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFormStore == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(pointOfInterestFormStore);

            return NoContent();
        }
    }
}
