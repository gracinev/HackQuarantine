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
        public static string[] SearchRequest { get; set; }
        public static string City { get; set; }
        private static int StoreId { get; set; }
        public static Store TempObj { get; set; }
        private readonly StockdDbContext _context;

        public HomeController(StockdDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Item item = new Item()
            //{
            //    Id = 0,
            //    Name = "idk",
            //    Price = 2,
            //    InStock = true,
            //    SaleStatus = SaleStatus.HighQuantity
            //};
            //Comment comment = new Comment()
            //{
            //    Id = 0,
            //    ItemId = 1,
            //    PlaceId = "hello",
            //    Date = DateTime.Now,
            //    Notes = "AAAAAAA"
            //};
            //_context.Item.Add(item);
            //_context.Comment.Add(comment);
            //_context.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult AddStores([FromBody]List<GoogleStore> stores)
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

        public IActionResult Store()
        {
            return View(TempObj);
        }

        [HttpPost]
        public IActionResult RedirectToStore([FromBody]string storeId)
        {
            //var id = int.Parse(storeId);
            Store store = Repository._stores.FirstOrDefault(s => s.PlaceId == storeId);
            TempObj = store;
            return Json("Success");
        }

        [HttpPost]
        public IActionResult Search(string search, string city)
        {
            if (search == null)
            {
                search = "";
            }
            if (city == null)
            {
                SearchRequest = new string[] { search, City };
            }
            else
            {
                SearchRequest = new string[] { search, city };
            }
            return RedirectToAction("Result");
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
