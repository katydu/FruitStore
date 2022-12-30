using System;
using FruitStore.Models;

namespace FruitStore.Helper;

public class CartClass
{
    public Products Product { get; set; }
    public OrderDetails OrderDetail { get; set; }
    public int Quantity { get; set; }
    public int TotalPrice { get; set; }

    public CartClass(Products product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        TotalPrice = product.ProductUnitPrice * quantity;
    }
    //public class OrderItem
    //{
    //    public int Id { get; set; }
    //    public int OrderId { get; set; }
    //    public int ProductId { get; set; }  //商品ID
    //    public int Amount { get; set; }     //數量
    //    public int SubTotal { get; set; }   //小計
    //}

    //public class CartItem : OrderItem
    //{
    //    public Products Products { get; set; } //商品內容
    //    public string imageSrc { get; set; } //商品圖片
    //}
}

