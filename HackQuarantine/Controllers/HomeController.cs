using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HackQuarantine.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Web;
using System.Net;
using Newtonsoft.Json;

// Classes we'll use from the GoogleApi package maybe?
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Places.Details.Request;
using GoogleApi.Entities.Places.Photos.Request;
using GoogleApi.Entities.Places.Search.NearBy.Request;
// End of Google API classes

namespace HackQuarantine.Controllers
{
    public class HomeController : Controller
    {
        static int[] idArray;
        public string[] SearchRequest { get; set; }
        public static string City { get; set; }
        private static int StoreId { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser([FromBody]List<GoogleStore> stores)
        {
            if (stores != null)
            {
                foreach (var store in stores)
                {
                    Repository.AddStore(store);
                }
                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }

        [HttpPost]
        public ActionResult GetCity([FromBody]string city)
        {
            if (city != null)
            {
                City = city;
                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }

        public IActionResult Privacy()
        {
            return View(Repository._stores);
        }

        public IActionResult Menu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string search, string city)
        {
            if (city == null)
            {
                //ModelState.AddModelError(String.Empty, "Please search a city.");
                SearchRequest = new string[] { search, City };
                //return View("Index");
            }
            else
            {
                SearchRequest = new string[] { search, city };
            }
            //SearchRequest = new string[] { search, city };
            //SearchRequest = new string[] { search, City };
            return View("Result", SearchRequest);
        }

        public IActionResult Result()
        {
            return View(SearchRequest);
        }

        public IActionResult AddComment(string storeId, string itemId)
        {
            var x = int.Parse(storeId);
            var y = int.Parse(itemId);
            idArray = new int[] { x, y };
            return View("CommentForm");
        }

        [HttpPost]
        public IActionResult AddItem(string storeId)
        {
            StoreId = int.Parse(storeId);
            return View("ItemForm");
        }

        [HttpGet]
        public IActionResult ItemForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult ItemForm(ItemRequest itemRequest)
        {
            itemRequest.StoreId = StoreId;
            if (ModelState.IsValid)
            {
                Repository.AddItem(itemRequest);
                return View("Privacy", Repository._stores);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CommentForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult CommentForm(CommentRequest commentRequest)
        {
            commentRequest.StoreId = idArray[0];
            commentRequest.ItemId = idArray[1];
            if (ModelState.IsValid)
            {
                Repository.AddComment(commentRequest);
                return View("Privacy", Repository._stores);
            }
            else
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

            
            return View(searchResponse.Results);
        }
    }
}
