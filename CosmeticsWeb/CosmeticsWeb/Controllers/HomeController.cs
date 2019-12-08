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
        private readonly CosmeticsService _cosmeticsService;

        public HomeController()
        {
            _cosmeticsService = new CosmeticsService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registe()
        {
            return View();
        }
        /// <summary>
        /// 显示指定数量的化妆品
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public ActionResult TopCosmetics(int number)
        {
            //取数据
            var model = _cosmeticsService.GetAllByNumber(number);
            return View(model);
        }
        public ActionResult CosmeticSearch()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SearchResult(string txtSearch)
        {
            var model = _cosmeticsService.GetAllSearchByName(txtSearch);
            return View(model);
        }
    }
}