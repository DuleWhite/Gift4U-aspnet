using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gift4U.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gift4U.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            if (HttpContext.Session.Keys.Contains("username")
                && HttpContext.Session.Keys.Contains("userid"))
            {
                HttpContext.Session.Remove("username");
                HttpContext.Session.Remove("userid");
            }
            if (HttpContext.Session.Keys.Contains("cartProducts"))
                HttpContext.Session.Remove("cartProducts");
            if (HttpContext.Session.Keys.Contains("code"))
                HttpContext.Session.Remove("code");
            CartProductManager.clear();
            return View();
        }
        public IActionResult Signup()
        {
            if (HttpContext.Session.Keys.Contains("username")
                && HttpContext.Session.Keys.Contains("userid"))
            {
                HttpContext.Session.Remove("username");
                HttpContext.Session.Remove("userid");
            }
            return View();
        }
        public string PhoneJudgement()
        {
            string phone ="" ;
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
                        phone = obj.phone;
                    }
                }
            }
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM users WHERE userphone='" + phone + "';";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read()) {
                dataReader.Close();
                return "true";
            }
            else {
                dataReader.Close();
                return "false";
            }
        }

        public string Message(){
            string phone = "";
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
                        phone = obj.phone;
                    }
                }
            }
            String code = getCode();
            //Send message to [phone] with [code].
            String result = SMSManager.send(phone, code);
            return result;
        }
        //Get a random code
        private String getCode()
        {
            Random rand = new Random();
            String code = "";
            for (int i = 0; i < 4; i++)
            {
                code += rand.Next(0, 9);
            }
            HttpContext.Session.SetString("code", code);
            return code;
        }

        public string UsernameJudgement(){
            string username = "";
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
                        username = obj.username;
                    }
                }
            }
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM users WHERE username='" + username + "';";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read()){
                dataReader.Close();
                return "true";
            }
            else{
                dataReader.Close();
                return "false";
            }
        }

        public string Code(){
            String code = "";
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
                        code = obj.code;
                    }
                }
            }
            if (HttpContext.Session.Keys.Contains("code") && HttpContext.Session.GetString("code") == code)
                return "true";
            else return "false";
        }

        public string _SignUp(){
            String username = "";
            String phone = "";
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
                        username = obj.username;
                        phone = obj.phone;
                    }
                }
            }
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "INSERT INTO users(username,userphone) VALUES ('" + username + "','" + phone + "');";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            int rows = command.ExecuteNonQuery();
            if (rows > 0)
            {
                HttpContext.Session.SetString("username", username);
                MySqlCommand command2 = new MySqlCommand();
                command2.CommandText = "SELECT userid FROM users WHERE username='" + username + "';";
                command2.Connection = connection;
                command2.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dataReader = command2.ExecuteReader();
                if(dataReader.Read()){
                    HttpContext.Session.SetString("userid", dataReader.GetString("userid"));
                    dataReader.Close();
                    return "true";
                }
                else{
                    dataReader.Close();
                    return "false";
                }
            }
            else return "false";
        }

        public string _Login(){
            string phone = "";
            string username = "";
            string userid = "";
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
                        phone = obj.phone;
                    }
                }
            }
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT username,userid FROM users WHERE userphone = '" + phone + "'; ";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                username = dataReader.GetString("username");
                userid = dataReader.GetString("userid");
                HttpContext.Session.SetString("userid", userid);
                HttpContext.Session.SetString("username", username);
                dataReader.Close();
                return "true";
            }
            else
            {
                dataReader.Close();
                return "false(1)";
            }
        }
    }
}
