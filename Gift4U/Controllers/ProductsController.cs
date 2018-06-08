using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Gift4U.Models;
using Gift4U.Util;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gift4U.Controllers
{
    public class ProductsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("username")
                && HttpContext.Session.Keys.Contains("userid"))
            {
                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewData["userid"] = HttpContext.Session.GetString("userid");
            }
            HttpContext.Session.SetString("prevpage", "Products");
            return View();
        }
        public IActionResult Product(string id)
        {
            if (HttpContext.Session.Keys.Contains("username")
                && HttpContext.Session.Keys.Contains("userid"))
            {
                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewData["userid"] = HttpContext.Session.GetString("userid");
            }
            ViewData["id"] = id;
            Product p = ProductsManager.getProductById(int.Parse(id));
            ViewData["Title"] = p.getProductName();
            ViewData["PrevPage"] = HttpContext.Session.GetString("prevpage");
            return View();
        }
        public string ProductInfo()
        {
            string ret = "";
            string productId = "";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<PostData>(requestBody);
                    if (obj != null)
                    {
                        productId = obj.productId;
                    }
                }
            }
            Product product = ProductsManager.getProductById(int.Parse(productId));
            ret = JsonConvert.SerializeObject(product);
            return ret;
        }
        public string AddToCart(){
            string productId = "";
            string color = "";
            string size = "";
            string quantity = "";
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<PostData>(requestBody);
                    if (obj != null)
                    {
                        productId = obj.productId;
                        color = obj.color;
                        size = obj.size;
                        quantity = obj.quantity;
                    }
                }
            }
            string cartProduct = productId + "-" + color + "-" + size + "-" + quantity;
            string cartProducts;
            string userid;
            if (HttpContext.Session.Keys.Contains("cartProducts"))
            {
                cartProducts = (string)HttpContext.Session.GetString("cartProducts");
                cartProducts += ",";
                cartProducts += cartProduct;
            }
            else cartProducts = cartProduct;
            if(HttpContext.Session.Keys.Contains("userid")){
                userid = (string)HttpContext.Session.GetString("userid");
            }
            else return "noLogin";
            HttpContext.Session.SetString("cartProducts", cartProducts);
            return "true";
        }
    }
}
