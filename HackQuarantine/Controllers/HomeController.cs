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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult CommentRequest()
        //{
        //    var model = new CommentRequest { };
        //    return PartialView("CommentModalPartial", model);
        //}

        //[HttpPost]
        //public IActionResult CommentRequest(CommentRequest model)
        //{
        //    return PartialView("CommentModalPartial", model);
        //}


        [HttpPost]
        public ViewResult Privacy(CommentRequest request)
        {
            if (ModelState.IsValid)
            {
                Repository.AddComment(request);
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
