using CosmeticsWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly CosmeticService _cosmeticService;

        public HomeController()
        {
            _cosmeticService = new CosmeticService();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 显示指定数量的化妆品
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public ActionResult TopCosmetics()
        {
            //取数据
            var model = _cosmeticService.GetAllByNumber(4);
            return View(model);
        }

        /// <summary>
        /// 进行搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult CosmeticSearch()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult SearchResult(string txtSearch)
        {
            var model = _cosmeticService.GetAllSearchByName(txtSearch);
            return View(model);
        }
    }
}