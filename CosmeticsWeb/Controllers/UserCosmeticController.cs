using CosmeticsWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    public class UserCosmeticController : Controller
    {
        private CosmeticService _cosmeticService;
        public UserCosmeticController()
        {
            _cosmeticService = new CosmeticService();
        }
        // GET: UserCosmetic
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///   化妆品详细页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(System.Guid id)
        {
            return View(_cosmeticService.GetById(id));
        }
    }
}