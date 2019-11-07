using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.ViewModels.AdminType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    public class AdminBookTypeController : Controller
    {
        // GET: AdminBookType
        public ActionResult Index()
        {

            var da = new CosmeticsEntities();
            var model = da.商品类型表;
            return View(model);
        }
        /// <summary>
        /// 新增分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new VmAdminTypeCreateModel();
            //model.商品类型名称 = "aaa"
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(VmAdminTypeCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //新增操作
            var newModel = new 商品类型表()
            {
                商品类型名称 = model.商品类型名称
            };
            //存入数据库
            var da = new CosmeticsEntities();
            da.商品类型表.Add(newModel);
            da.SaveChanges();

            //返回列表页
            return RedirectToAction("index");


        }
        /// <summary>
        /// 对新增类型名称进行检验，如果已经存在返回false
        /// </summary>
        /// <param name="商品类型名称"></param>
        /// <returns></returns>
        public ActionResult RemoteValidateForNewType(string 商品类型名称)
        {
            var da = new CosmeticsEntities();
            var answer = !da.商品类型表.Any(m => m.商品类型名称 == 商品类型名称);
            return Json(answer, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string typeName)
        {
            try
            {
                var da = new CosmeticsEntities();
                var model = da.商品类型表.FirstOrDefault(m => m.商品类型名称 == typeName);
                if (model == null)
                {
                    //没有对应记录
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                //检查这个分类名称是否已经使用
                if(da.商品信息表.Any(m=>m.商品类型名称== typeName))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                da.商品类型表.Remove(model);
                da.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(false, JsonRequestBehavior.AllowGet); }
        }
    }
}