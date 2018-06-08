using System;
namespace Gift4U.Models
{
    public class CartProduct
    {
        private int productId;
        private string productColor;
        private string productSize;
        private int quantity;
        private string[] productImages;
        private string productName;
        private double productPrice;
        public CartProduct()
        {
        }
        public CartProduct(int productId, string productColor, string productSize, int quantity)
        {
            this.productId = productId;
            this.productColor = productColor;
            this.productSize = productSize;
            this.quantity = quantity;
        }

        public bool hasColor()
        {
            return productColor != null && productColor != "" && productColor != "null";
        }

        public bool hasSize()
        {
            return productSize != null && productSize != "" && productSize != "null";
        }

        public string[] getProductImages()
        {
            return productImages;
        }

        public void setProductImages(string[] productImages)
        {
            this.productImages = productImages;
        }

        public string getProductName()
        {
            return productName;
        }

        public void setProductName(string productName)
        {
            this.productName = productName;
        }

        public double getProductPrice()
        {
            return productPrice;
        }

        public void setProductPrice(double productPrice)
        {
            this.productPrice = productPrice;
        }

        public int getQuantity()
        {
            return quantity;
        }

        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public int getProductId()
        {
            return productId;
        }

        public void setProductId(int productId)
        {
            this.productId = productId;
        }

        public string getProductColor()
        {
            return productColor;
        }

        public void setProductColor(string productColor)
        {
            this.productColor = productColor;
        }

        public string getProductSize()
        {
            return productSize;
        }

        public void setProductSize(string productSize)
        {
            this.productSize = productSize;
        }

    }
}
