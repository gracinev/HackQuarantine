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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View(Repository._stores);
        }

        [HttpPost]
        public IActionResult Privacy(string storeId, string itemId)
        {
            var x = int.Parse(storeId);
            var y = int.Parse(itemId);
            idArray = new int[] { x, y };
            return View("CommentForm");
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
                return View("Privacy");
            }
            else
            {
                return View();
            }
        }
    }
}
