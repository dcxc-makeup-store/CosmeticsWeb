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
        private readonly CosmeticsService _cosmeticsService;

        public UserCosmeticController()
        {
            _cosmeticsService = new CosmeticsService();
        }


        // GET: UserCosmetic
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///  详细页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(System.Guid id)
        {

            return View(_cosmeticsService.GetById(id));
           
        }
    }
}