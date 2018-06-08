using System;
using System.Collections.Generic;
namespace Gift4U.Util
{
    public class PageConnectionManager
    {
        public static string getPageHref(string pageName)
        {
            switch(pageName){
            case "Home":
                return "/";
            case "Products":
                return "/Products";
            case "Orders":
                return "/Orders";
            case "Cart":
                return "/Cart";
            }
            return "";
        }
    }
}
