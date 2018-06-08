using System;
using System.Collections.Generic;

namespace Gift4U.Models
{
    public class Order
    {
        public class ProductParam
        {
            public int productId;
            public int quantity;
            public String selectedColor;
            public String selectedSize;
            public ProductParam()
            {

            }
        }
        private int orderId { get; set; }
        private double totalPrice { get; set; }
        private string shippingTo { get; set; }
        private DateTime orderDate { get; set; }
        private string OrderStatus { get; set; }
        private List<ProductParam> products { get; set; }
        private string orderNote { get; set; }
        public Order()
        {
        }

        public int getOrderId()
        {
            return orderId;
        }

        public void setOrderId(int orderId)
        {
            this.orderId = orderId;
        }

        public double getTotalPrice()
        {
            return totalPrice;
        }

        public void setTotalPrice(double totalPrice)
        {
            this.totalPrice = totalPrice;
        }

        public string getShippingTo()
        {
            return shippingTo;
        }

        public void setShippingTo(string shippingTo)
        {
            this.shippingTo = shippingTo;
        }

        public DateTime getOrderDate()
        {
            return orderDate;
        }

        public void setOrderDate(DateTime orderDate)
        {
            this.orderDate = orderDate;
        }

        public string getOrderStatus()
        {
            return OrderStatus;
        }

        public void setOrderStatus(string orderStatus)
        {
            OrderStatus = orderStatus;
        }

        public List<ProductParam> getProducts()
        {
            return products;
        }

        public void setProducts(List<ProductParam> products)
        {
            this.products = products;
        }

        public string getOrderNote(){
            return orderNote;
        }

        public void setOrderNote(string orderNote){
            this.orderNote = orderNote;
        }
    }
}
