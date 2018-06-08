using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.IO;
using Gift4U.Util;
using Newtonsoft.Json;
using Gift4U.Models;
using MySql.Data.MySqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gift4U.Controllers
{
    public class CartController : Controller
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
            prevPage = prevPage.Split("-")[0] + "-Cart";
            HttpContext.Session.SetString("prevpage", prevPage);
            ViewData["cartProducts"] = HttpContext.Session.GetString("cartProducts");
            HttpContext.Session.Remove("cartProducts");
            return View();
        }

        public string Checkout(){
            string userid = HttpContext.Session.GetString("userid");
            string shippingTo = "";
            string orderNote = "";
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
                        shippingTo = obj.shippingTo;
                        orderNote = obj.notetext;
                    }
                }
            }
            double totalPrice = CartProductManager.getTotalPrice();
            string dateString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string orderStatus = "Paid";
            List<CartProduct> products = CartProductManager.getCartProducts();
            string orderid = "";

            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "INSERT INTO orders(userid,totalprice,shippingto,orderdate,orderstatus,ordernote) VALUES(" + userid + "," + totalPrice + ",'" + shippingTo + "','" + dateString + "','" + orderStatus + "','" + orderNote + "');";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            int rows = command.ExecuteNonQuery();
            if (rows <= 0)
            {
                return "false";
            }
            command.CommandText = "SELECT orderid FROM orders WHERE userid=" + userid + " AND orderdate='" + dateString + "'";
            MySqlDataReader dataReader = command.ExecuteReader();
            if(dataReader.Read()){
                orderid = dataReader.GetString("orderid");
                dataReader.Close();
            }
            else{
                dataReader.Close();
                return "false";
            }
            foreach (CartProduct cartProduct in products)
            {
                string productColor = "";
                if(cartProduct.getProductColor()=="null"){
                    productColor = "";
                }
                else{
                    productColor = cartProduct.getProductColor();
                }
                string productSize = "";
                if(cartProduct.getProductSize()=="null"){
                    productSize = "";
                }
                else{
                    productSize = cartProduct.getProductSize();
                }
                command.CommandText = "INSERT INTO order_product(orderid,productid,quantity,selectedcolor,selectedsize) VALUES(" + orderid + "," + cartProduct.getProductId() + "," + cartProduct.getQuantity() + ",'" + productColor + "','" + productSize + "');";
                rows = command.ExecuteNonQuery();
                if (rows <= 0)
                {
                    return "false";
                }
            }
            CartProductManager.clear();
            return (orderid + "-" + totalPrice);
        }

        public string RemoveCartProduct(){
            int productId = -1;
            string productColor = "";
            string productSize = "";
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
                        productId = int.Parse(obj.productId);
                        productColor = obj.productColor;
                        productSize = obj.productSize;
                    }
                }
            }
            CartProductManager.removeProduct(productId, productColor, productSize);
            double totalPrice = CartProductManager.getTotalPrice();
            if (CartProductManager.getCount() == 0) return("empty");
            else return ("" + totalPrice);
        }

        public string UpdateCartProductQuantity(){
            int productId = -1;
            String productColor = "";
            String productSize = "";
            int quantity = -1;
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
                        productId = int.Parse(obj.productId);
                        productColor = obj.productColor;
                        productSize = obj.productSize;
                        quantity = int.Parse(obj.quantity);
                    }
                }
            }
            double newPrice = CartProductManager.updateCartProductQuantity(productId, productColor, productSize, quantity);
            return (newPrice + "-" + CartProductManager.getTotalPrice());
        }
    }
}
