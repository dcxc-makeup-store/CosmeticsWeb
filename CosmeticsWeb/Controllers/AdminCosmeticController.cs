using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.ViewModels.AdminCosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    /// <summary>
    /// 后台化妆品管理
    /// </summary>
    public class AdminCosmeticController : Controller
    {
        // GET: AdminCosmetic
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        /// <summary>
        /// 所有化妆品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var da = new CosmeticsEntities();
            var model = da.商品信息表;
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new VmAdminCosmeticCreate();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(VmAdminCosmeticCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //新增操作
            var newModel = new 商品信息表()
            {
                商品名称 = model.商品名称,
                商品类型名称 = model.商品类型名称,
                商品单价 = model.商品单价,
                商品规格=model.商品规格,
                商品库存=model.商品库存,
                商品描述=model.商品描述,
                商品品牌=model.商品品牌,
            };
            //存入数据库
            var da = new CosmeticsEntities();
            da.商品信息表.Add(newModel);
            da.SaveChanges();

            //返回列表页
            return RedirectToAction("List");


        }
        /// <summary>
        /// 对新增类型名称进行检验，如果已经存在返回false
        /// </summary>
        /// <param name="商品名称"></param>
        /// <returns></returns>
        public ActionResult RemoteValidateForNewCosmeticName(string 商品名称)
        {
            var da = new CosmeticsEntities();
            var answer = !da.商品信息表.Any(m => m.商品名称 == 商品名称);
            return Json(answer, JsonRequestBehavior.AllowGet);
        }
        
    }
}