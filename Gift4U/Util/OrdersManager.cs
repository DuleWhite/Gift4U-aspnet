using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Gift4U.Models;

namespace Gift4U.Util
{
    public class OrdersManager
    {
        public OrdersManager()
        {
        }

        public static List<Order> getOrdersById(int userid)
        {
            List<Order> orders = new List<Order>();
            MySqlConnection connection = DBManager.getManager().getConnection();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM orders WHERE userid = " + userid + "  ORDER BY orderdate DESC;";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read()){
                Order order = new Order();
                order.setOrderId(dataReader.GetInt32("orderid"));
                order.setTotalPrice(dataReader.GetDouble("totalprice"));
                order.setShippingTo((string)dataReader["shippingto"]);
                order.setOrderDate(dataReader.GetDateTime("orderDate"));
                order.setOrderStatus((string)dataReader["orderstatus"]);
                order.setOrderNote((string)dataReader["ordernote"]);
                orders.Add(order);
            }
            dataReader.Close();
            foreach(Order order in orders){
                MySqlCommand command2 = new MySqlCommand();
                command2.CommandText = "SELECT * FROM order_product WHERE orderid=" + order.getOrderId() + ";";
                command2.Connection = connection;
                command2.CommandType = System.Data.CommandType.Text;
                MySqlDataReader dataReader2 = command2.ExecuteReader();
                List<Order.ProductParam> products = new List<Order.ProductParam>();
                while (dataReader2.Read())
                {
                    Order.ProductParam productParam = new Order.ProductParam();
                    productParam.productId = dataReader2.GetInt32("productid");
                    productParam.quantity = dataReader2.GetInt32("quantity");
                    productParam.selectedColor = (string)dataReader2["selectedcolor"];
                    productParam.selectedSize = (string)dataReader2["selectedsize"];
                    products.Add(productParam);
                }
                order.setProducts(products);
                dataReader2.Close();
            }
            return orders;
        }
    }
}
