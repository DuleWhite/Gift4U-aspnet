using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Gift4U.Models;
using Gift4U.Util;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Gift4U.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("username")
                && HttpContext.Session.Keys.Contains("userid"))
            {
                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewData["userid"] = HttpContext.Session.GetString("userid");
            }
            HttpContext.Session.SetString("prevpage", "Home");
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class PostData
    {
        public string productId { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string quantity { get; set; }
        public string id { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public string code { get; set; }
        public string password { get; set; }
        public string productColor { get; set; }
        public string productSize { get; set; }
        public string userid { get; set; }
        public string shippingTo { get; set; }
        public string notetext { get; set; }
        public string orderid { get; set; }
    }
}
