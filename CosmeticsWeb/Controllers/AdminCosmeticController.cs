using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.Main;
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
            try
            {
                //输入校验
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //新增操作 向数据库插入数据
                var newModel = new 商品信息表();

                newModel.商品ID = Guid.NewGuid().ToString();
                // 将VmAdminBookCreate类型的model变量里的内容复制到newModel
                Util.CopyObjectData(model, newModel, "商品ID");
                //存入数据库
                var da = new CosmeticsEntities();
                da.商品信息表.Add(newModel);
                da.SaveChanges();

                //返回列表页 转向
                return RedirectToAction("List");


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }


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