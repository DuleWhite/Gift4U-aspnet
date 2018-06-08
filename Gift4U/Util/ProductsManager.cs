using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Gift4U.Models;
namespace Gift4U.Util
{
    public class ProductsManager
    {
        public ProductsManager()
        {
        }

        public static List<Product> getProducts()
        {
            List<Product> products = new List<Product>();
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM products;";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read()) {
                Product product = getProductFromDataReader(dataReader);
                products.Add(product);
            }
            dataReader.Close();
            return products;
        }

        public static Product getProductById(int productid)
        {
            Product product = null;
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM products WHERE productid=" + productid + ";";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dataReader = command.ExecuteReader();
            if(dataReader.Read())
                product = getProductFromDataReader(dataReader);
            dataReader.Close();
            return product;
        }

        private static Product getProductFromDataReader(MySqlDataReader dataReader)
        {
            Product product = new Product();

            product.setProductId(dataReader.GetInt32("productid"));
            product.setProductName((string)dataReader["productname"]);
            string imageName = (string)dataReader["productimgname"];
            int imageCount = dataReader.GetInt32("productimgcount");
            string imageFormat = (string)dataReader["productimgformat"];
            string[] strings = new string[imageCount];
            for (int i = 1; i <= imageCount; i++)
            {
                strings[i - 1] = imageName + "-" + i + "-s." + imageFormat;
            }
            product.setProductImages(strings);
            product.setProductPrice(dataReader.GetDouble("productprice"));
            String colorString = (string)dataReader["productcolor"];
            if (colorString != null && colorString!="")
            {
                product.setProductColors(colorString.Split(","));
            }
            String sizeString = (string)dataReader["productsize"];
            if (sizeString != null && sizeString!="")
                product.setProductSizes(sizeString.Split(","));
            product.setProductDescription((string)dataReader["productdescription"]);
            product.setProductInfo((string)dataReader["productinfo"]);
            product.setProductReturnAndRefeundPolicy((string)dataReader["productreturn_refeundpolicy"]);
            product.setProductShippingInfo((string)dataReader["productshippinginfo"]);
            String tagString = (string)dataReader["producttag"];
            if (tagString != null && tagString!="")
                product.setProductTag(tagString);
            return product;
        }
    }
}
