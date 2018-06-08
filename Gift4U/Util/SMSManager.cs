using System;
using System.Security.Cryptography;
using System.Text;

namespace Gift4U.Util
{
    public class SMSManager
    {
        public SMSManager()
        {
        }
        public static string send(string phoneNumber, string code){
            System.Net.WebClient client = new System.Net.WebClient();
            client.Credentials = System.Net.CredentialCache.DefaultCredentials;
            string username = "Gift4U";
            MD5 md5Hash = MD5.Create();
            string password = "Gift4U";
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++) {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            password = stringBuilder.ToString();
            string content = "[Gift4U] Your Code: " + code + ".";
            content = System.Web.HttpUtility.UrlEncode(content, Encoding.UTF8);

            //Normal Mode
            byte[] result = (client.DownloadData("http://api.smsbao.com/sms?u=" + username + "&p=" + password + "&m=" + phoneNumber + "&c=" + content));

            //Dubug Mode
            //result = "0";
            string ret = Encoding.UTF8.GetString(result);
            return ret;
        }
    }
}
