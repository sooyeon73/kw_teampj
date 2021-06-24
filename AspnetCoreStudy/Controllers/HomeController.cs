using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreStudy.Models;
using Microsoft.AspNetCore.Http;


namespace AspnetCoreStudy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //var firstUser = new User();
            //firstUser.UserNo = 1;
            //firstUser.UserName = "홍길동";

            var hongUser = new User
            {
                UserNo = 1,
                UserName = "홍길동"
            };

            //return View();

            // # 1번째 방식 View(Model)
            //return View(hongUser);


            // # 2번째 방식 ViewBag
            //ViewBag.User = hongUser;
            //return View();

            // # 3번째 방식 ViewData
            ViewData["UserNo"] = hongUser.UserNo;
            ViewData["UserName"] = hongUser.UserName;

            return View();
        }

        public IActionResult LoginSucess()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
