using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Gift4U.Util;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gift4U.Controllers
{
    public class OrdersController : Controller
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
            string prevPage = HttpContext.Session.GetString("prevpage");
            prevPage = prevPage.Split("-")[0] + "-Orders";
            HttpContext.Session.SetString("prevpage", prevPage);
            ViewData["userid"] = HttpContext.Session.GetString("userid");
            return View();
        }
        public IActionResult Thanks(string orderId, string totalPrice, string shippingTo1, string shippingTo2)
        {
            if (HttpContext.Session.Keys.Contains("username")
                && HttpContext.Session.Keys.Contains("userid"))
            {
                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewData["userid"] = HttpContext.Session.GetString("userid");
            }
            ViewData["orderId"] = orderId;
            ViewData["totalPrice"] = totalPrice;
            ViewData["shippingTo1"] = shippingTo1;
            ViewData["shippingTo2"] = shippingTo2;
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }

        public string ConfirmReceipt(){
            String orderid = "";
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
                        orderid = obj.orderid;
                    }
                }
            }
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "UPDATE orders SET orderstatus='Done' WHERE orderid=" + orderid + ";";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            int rows = command.ExecuteNonQuery();
            if (rows != 0)
            {
                return "true";
            }
            else{
                return "false";
            }
        }

        public string ConfirmReceiptAll(){
            string userid = HttpContext.Session.GetString("userid");
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "UPDATE orders SET orderstatus='Done' WHERE userid=" + userid + " AND orderstatus='Delivery';";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            int rows = command.ExecuteNonQuery();
            if (rows > 0)
            {
                return "true";
            }
            else{
                return "false";
            }
        }
    }
}
