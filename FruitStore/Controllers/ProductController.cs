using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitStore.Data;
using Microsoft.AspNetCore.Mvc;
using FruitStore.Models;
using FruitStore.Helper;
using static FruitStore.Helper.CartClass;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;

namespace FruitStore.Controllers;

public class ProductController : Controller
{

    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<FruitStore.Models.Products> products = _db.Products;
        return View(products);
    }

    [HttpPost]
    public IActionResult Index(int id)
    {
        var ProductData = _db.Products.Find(id);
        return View(ProductData);
    }

    
    public IActionResult AddCart(int id,int quantity)
    {
        CartClass item = new CartClass(_db.Products.Find(id), quantity);
        // does session has cart or not
        if(SessionHelper.GetObjectFromJson<List<CartClass>>(HttpContext.Session, "cart") == null)
        {
            //if null then add a new one
            List<CartClass> cart = new List<CartClass>();
            cart.Add(item);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        }
        else
        {
            // if exist check the item is the same or not
            List<CartClass> cart = SessionHelper.GetObjectFromJson<List<CartClass>>(HttpContext.Session, "cart");
            // FindIndex can find where is the item's place in the list
            int index = cart.FindIndex(m => m.Product.ProductID.Equals(id));
            if(index != -1)
            {
                // if same then add the number
                cart[index].Quantity += item.Quantity;
                cart[index].TotalPrice += item.TotalPrice;
            }
            else
            {
                // if is different add new item in cart
                cart.Add(item);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            TempData["success"] = "已新增商品至購物車！";

        }
        // httpstatus 204: request successfully but don't refresh the view
        return NoContent();
    }

    [HttpGet]
    public IActionResult Cart()
    {
        // get the cart from session
        List<CartClass> CartItems = SessionHelper.GetObjectFromJson<List<CartClass>>(HttpContext.Session, "cart");

        if(CartItems != null)
        {
            ViewBag.Total = CartItems.Sum(m => m.TotalPrice);
        }
        else
        {
            TempData["warning"] = "購物車沒有商品！";

            // todo I don't want to have the refresh action on the website to reach the goal of refresh
            //var url = HttpContext.Request.GetEncodedUrl();

            return View("Index");
        }

        return View(CartItems);
    }

    


    public IActionResult RemoveItem(int id)
    {
        //向 Session 取得商品列表
        List<CartClass> cart = SessionHelper.GetObjectFromJson<List<CartClass>>(HttpContext.Session, "cart");

        //用FindIndex查詢目標在List裡的位置
        int index = cart.FindIndex(m => m.Product.ProductID.Equals(id));
        cart.RemoveAt(index);

        if (cart.Count < 1)
        {
            SessionHelper.Remove(HttpContext.Session, "cart");
        }
        else
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        }
        TempData["success"] = "已刪除商品！";
        return RedirectToAction("Cart","Product");
    }

    public IActionResult EditItem(int id, int quantity)
    {
        List<CartClass> cart = SessionHelper.GetObjectFromJson<List<CartClass>>(HttpContext.Session, "cart");
        int index = cart.FindIndex(m => m.Product.ProductID.Equals(id));
        cart[index].TotalPrice = quantity * cart[index].Product.ProductUnitPrice;
        cart[index].Quantity = quantity;
        //cart[index].TotalPrice = Content.

        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        TempData["success"] = "已更改商品！";
        return RedirectToAction("Cart");
    }

}

