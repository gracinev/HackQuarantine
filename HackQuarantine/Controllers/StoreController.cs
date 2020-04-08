using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Classes we'll use from the GoogleApi package maybe?
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Places.Details.Request;
using GoogleApi.Entities.Places.Photos.Request;
using GoogleApi.Entities.Places.Search.NearBy.Request;
// End of Google API classes


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackQuarantine.Controllers
{
    public class StoreController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }

    // Trying some Google Maps stuff below
    // From https://github.com/vivet/GoogleApi
    // From https://www.jerriepelser.com/tutorials/airport-explorer/google-places/retrieving/
    // but it's for Razor pages?
    // Looked at https://www.youtube.com/watch?v=3NLfhEjq9Tk
    // GET Home/StoreDetail?
    public async Task<IActionResult> StoreDetail()
    {
        // For testing the call
        double latitude = 37.424831;
        double longitude = -121.867796;
        var searchResponse = await GooglePlaces.NearBySearch.QueryAsync(new PlacesNearBySearchRequest
        {
            Key = "AIzaSyBqAlYsHSmQ7giaR-rSd9tLRZhztugU004",
            Location = new GoogleApi.Entities.Places.Search.NearBy.Request.Location(latitude, longitude),
            Radius = 10000
        });

        if (!searchResponse.Status.HasValue || searchResponse.Status.Value != Status.Ok || !searchResponse.Results.Any())
            return new BadRequestResult();

        // How do I pass response to View()
        // I can't find documentation on this nuget package 
        return View(searchResponse.Results);
    }
