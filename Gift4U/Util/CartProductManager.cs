using System;
using System.Collections.Generic;
using Gift4U.Models;

namespace Gift4U.Util
{
    public class CartProductManager
    {
        private static List<CartProduct> cartProducts = new List<CartProduct>();

        public static double getTotalPrice()
        {
            double totalPrice = 0;
            foreach (CartProduct cartProduct in cartProducts)
            {
                totalPrice += cartProduct.getProductPrice() * cartProduct.getQuantity();
            }
            return totalPrice;
        }

        public static int getCount()
        {
            return cartProducts.Count;
        }

        public CartProductManager()
        {

        }

        public static void updateCartProducts(String cartProductsString)
        {
            //clear();
            String[]
            cartProductsArray = new string[0];
            if (cartProductsString != null && cartProductsString != "")
            {
                cartProductsArray = cartProductsString.Split(",");
            }
            foreach (String cp in cartProductsArray)
            {
                String[] cartProductDetails = cp.Split("-");
                addProduct(int.Parse(cartProductDetails[0]), cartProductDetails[1], cartProductDetails[2], int.Parse(cartProductDetails[3]));
            }
        }
        public static String getLastesCartProductsString()
        {
            String ret = "";
            foreach (CartProduct cp in cartProducts)
            {
                String str = "";
                str += cp.getProductId();
                str += "-";
                str += cp.getProductColor();
                str += "-";
                str += cp.getProductSize();
                str += "-";
                str += cp.getQuantity();
                if (ret == "") ret += str;
                else ret += "," + str;
            }
            return ret;
        }

        public static void clear()
        {
            cartProducts = new List<CartProduct>();
        }
        private static void addProduct(int productId, String color, String size, int quantity)
        {
            foreach (CartProduct cartProduct in cartProducts)
            {
                if (productId == cartProduct.getProductId() && color == cartProduct.getProductColor() && size == cartProduct.getProductSize())
                {
                    cartProduct.setQuantity(cartProduct.getQuantity() + quantity);
                    return;
                }
            }
            CartProduct cp = new CartProduct(productId, color, size, quantity);
            Product p = ProductsManager.getProductById(productId);
            cp.setProductImages(p.getProductImages());
            cp.setProductName(p.getProductName());
            cp.setProductPrice(p.getProductPrice());
            cartProducts.Add(cp);
        }

        public static List<CartProduct> getCartProducts()
        {
            return cartProducts;
        }

        public static void setCartProducts(List<CartProduct> cartProducts)
        {
            CartProductManager.cartProducts = cartProducts;
        }

        public static void removeProduct(int productId, String productColor, String productSize)
        {
            foreach (CartProduct cartProduct in cartProducts)
            {
                if (cartProduct.getProductId() == productId)
                {
                    if (cartProduct.hasColor() && cartProduct.hasSize() && productColor==cartProduct.getProductColor() && productSize==cartProduct.getProductSize())
                    {
                        cartProducts.Remove(cartProduct);
                        return;
                    }
                    if (cartProduct.hasColor() && !cartProduct.hasSize() && productColor==cartProduct.getProductColor())
                    {
                        cartProducts.Remove(cartProduct);
                        return;
                    }
                    if (cartProduct.hasSize() && !cartProduct.hasColor() && productSize==cartProduct.getProductSize())
                    {
                        cartProducts.Remove(cartProduct);
                        return;
                    }
                    if (!cartProduct.hasColor() && !cartProduct.hasSize())
                    {
                        cartProducts.Remove(cartProduct);
                        return;
                    }
                }
            }
        }

        public static void print()
        {
            Console.WriteLine("print:");
            foreach (CartProduct cp in cartProducts)
            {
                Console.Write(cp.getProductId() + "-");
                Console.Write(cp.getProductColor() + "-");
                Console.Write(cp.getProductSize() + "-");
                Console.Write(cp.getQuantity());
                Console.WriteLine();
            }
            Console.WriteLine(getCount());
        }

        public static double updateCartProductQuantity(int productId, String color, String size, int quantity)
        {
            Console.WriteLine("updatequantity recived: " + productId + " " + color + " " + size + " " + quantity);
            foreach (CartProduct cp in cartProducts)
            {
                if (cp.getProductId() == productId
                        && (cp.getProductColor()==color || (cp.getProductColor()=="null" && color==""))
                        && (cp.getProductSize()==size || (cp.getProductSize()=="null" && size==""))
                )
                {
                    cp.setQuantity(quantity);
                    Console.WriteLine("updatequantity:" + cp.getProductId() + "," + cp.getQuantity());
                    return cp.getProductPrice() * cp.getQuantity();
                }
            }
            return -1;
        }
    }
}
