using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HackQuarantine.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HackQuarantine.Controllers
{
    public class HomeController : Controller
    {
        static int[] idArray;
        public string[] SearchRequest { get; set; }
        private static int StoreId { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(Repository._stores);
        }

        public IActionResult Search(string search, string city)
        {
            if (city == null)
            {
                ModelState.AddModelError(String.Empty, "Please search a city.");
                return View("Index");
            }
            SearchRequest = new string[] { search, city };
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
    }
}
