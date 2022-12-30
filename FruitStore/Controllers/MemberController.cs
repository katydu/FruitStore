using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FruitStore.Data;
using FruitStore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
//e-mail validation
using System.Net.Mail;

namespace FruitStore.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MemberController(ApplicationDbContext db)
        {
            _db = db;
        }

        public static bool IsValidEmail(string email)
        {
            if (!MailAddress.TryCreate(email, out var mailAddress))
                return false;

            // And if you want to be more strict:
            var hostParts = mailAddress.Host.Split('.');
            if (hostParts.Length == 1)
                return false; // No dot.
            if (hostParts.Any(p => p == string.Empty))
                return false; // Double dot.
            if (hostParts[^1].Length < 2)
                return false; // TLD only one letter.

            if (mailAddress.User.Contains(' '))
                return false;
            if (mailAddress.User.Split('.').Any(p => p == string.Empty))
                return false; // Double dot or dot at end of user part.

            return true;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Members members)
        {
            //todo 要寫防呆已經註冊過的會員不然這裡會不給過
            var memberData = _db.Members.SingleOrDefault(u => u.MemberPhone == members.MemberPhone);

            if(memberData == null)
            {
                NotFound();
                return View();
            }
            else if(members.MemberPassword != memberData.MemberPassword)
            {
                NotFound();
                return View();
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, memberData.MemberName),
                new Claim(ClaimTypes.MobilePhone, members.MemberPhone),
                new Claim("FullName", memberData.MemberName),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var Principal = new ClaimsPrincipal(claimsIdentity);
            //var Properties = new AuthenticationProperties
            //{
            //};
            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                // 這裡可以自訂驗證選項...
                // 是否可自動更新 Cookie(時效?)
                //AllowRefresh = <bool>,
                // IsPersistent 設置 Persistent cookies，否則瀏覽器 session 關閉就失效
                IsPersistent = true,
                //AllowRefresh = true,
                // Persistent cookie 可進一步設置失效時間：
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //IssuedUtc = <DateTimeOffset>,
                //RedirectUri = <string>
            });
            TempData["success"] = "登入成功！";
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Members members)
        {
            if (ModelState.IsValid)
            {
                if(IsValidEmail(members.MemberEmail) == false)
                {
                    ModelState.AddModelError("MemberEmail", "Format incorrect");
                    return View();
                }

                _db.Members.Add(members);
                _db.SaveChanges();
                ModelState.Clear();
                TempData["success"] = "註冊成功！";
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData["success"] = "登出成功！";
            return RedirectToAction("Index", "Home");
        }
    }
}

