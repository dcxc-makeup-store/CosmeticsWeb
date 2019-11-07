using CosmeticsWeb.Models.ViewModels.AdminLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    public class AdminLoginController : Controller
    {
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]//点到登录之后 填的内容也还会返回在
        public ActionResult Index(VmAdminLogin model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Password == Properties.Settings.Default.AdminPassword)
            {
                //跳到AdminHome
                return RedirectToAction("AdminHome");
            }
            ModelState.AddModelError("", "密码错误！");
            return View(model);
        }

        public ActionResult AdminHome()
        {
            return View();
        }
     
    }
}