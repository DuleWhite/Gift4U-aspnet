using System;
namespace Gift4U.Models
{
    public class Product
    {
        public int productId;
        public string productName;
        public string[] productImages;
        public double productPrice;
        public string[] productColors;
        public string[] productSizes;
        public string productDescription;
        public string productInfo;
        public string productReturnAndRefeundPolicy;
        public string productShippingInfo;
        public string productTag;
        public Product()
        {
        }
        public int getProductId()
        {
            return productId;
        }

        public void setProductId(int productId)
        {
            this.productId = productId;
        }

        public string getProductName()
        {
            return productName;
        }

        public void setProductName(string productName)
        {
            this.productName = productName;
        }

        public string[] getProductImages()
        {
            return productImages;
        }

        public void setProductImages(string[] productImages)
        {
            this.productImages = productImages;
        }

        public double getProductPrice()
        {
            return productPrice;
        }

        public void setProductPrice(double productPrice)
        {
            this.productPrice = productPrice;
        }

        public string[] getProductColors()
        {
            return productColors;
        }

        public void setProductColors(string[] productColors)
        {
            this.productColors = productColors;
        }

        public string[] getProductSizes()
        {
            return productSizes;
        }

        public void setProductSizes(string[] productSizes)
        {
            this.productSizes = productSizes;
        }

        public string getProductDescription()
        {
            return productDescription;
        }

        public void setProductDescription(string productDescription)
        {
            this.productDescription = productDescription;
        }

        public string getProductInfo()
        {
            return productInfo;
        }

        public void setProductInfo(string productInfo)
        {
            this.productInfo = productInfo;
        }

        public string getProductReturnAndRefeundPolicy()
        {
            return productReturnAndRefeundPolicy;
        }

        public void setProductReturnAndRefeundPolicy(string productReturnAndRefeundPolicy)
        {
            this.productReturnAndRefeundPolicy = productReturnAndRefeundPolicy;
        }

        public string getProductShippingInfo()
        {
            return productShippingInfo;
        }

        public void setProductShippingInfo(string productShippingInfo)
        {
            this.productShippingInfo = productShippingInfo;
        }

        public string getProductTag()
        {
            return productTag;
        }

        public void setProductTag(string productTag)
        {
            this.productTag = productTag;
        }
    }
}
