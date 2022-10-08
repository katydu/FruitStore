using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FruitStore.Controllers
{
    public class StoreDataController : Controller
    {
        readonly Models.MembersData membersData = new Models.MembersData();
        readonly Models.MemberService memberService = new Models.MemberService();

        [HttpGet()]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public ActionResult MemberLogIn()
        {
            return View();
        }

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MemberLogIn(Models.MembersData membersData)
        {

            return View();
        }

        [HttpGet()]
        public ActionResult MemberRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MemberRegister(Models.MembersData membersData)
        {

            if (ModelState.IsValid)
            {
                this.memberService.AddMember(membersData);
                //TempData["message"] = "書籍 (" + membersData.MemberName + ") 註冊成功";
                ModelState.Clear();
                return View("MemberLogIn");
            }
            return View();
        }

    }
}