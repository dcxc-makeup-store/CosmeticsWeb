using CosmeticsWeb.Models.ViewModels.Account;
using CosmeticsWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService();
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(VmRegister model)
        {
            try
            {
                //输入校验
                if (!ModelState.IsValid) return View(model);
                //向数据库插入数据
                _accountService.Create(model);
                //转向
                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        /// <summary>
        /// 判断某个用户名是否已经使用
        /// </summary>
        /// <param name="用户名"></param>
        /// <returns></returns>
        public ActionResult RemoteValidateIsNameUsed(string 用户名)
        {
            return Json(!_accountService.IsNameUsed(用户名), JsonRequestBehavior.AllowGet);
        }
    }
}