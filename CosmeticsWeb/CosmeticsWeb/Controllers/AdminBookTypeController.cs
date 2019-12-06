using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.ViewModels.AdminType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Type = CosmeticsWeb.Models.EF.Type;

namespace CosmeticsWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBookTypeController : Controller
    {
        // GET: AdminBookType
        public ActionResult Index()
        {

            var da = new CosmeticEntities();
            var model = da.Type;
            return View(model);
        }
        /// <summary>
        /// 新增分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new VmAdminTypeCreate();
            //model.商品类型名称 = "aaa"
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(VmAdminTypeCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //新增操作
            var newModel = new Type()
            {
                CosmeticType = model.CosmeticType
            };
            //存入数据库
            var da = new CosmeticEntities();
            da.Type.Add(newModel);
            da.SaveChanges();

            //返回列表页
            return RedirectToAction("index");


        }
        /// <summary>
        /// 对新增类型名称进行检验，如果已经存在返回false
        /// </summary>
        /// <param name="商品类型名称"></param>
        /// <returns></returns>
        public ActionResult RemoteValidateForNewType(string CosmeticType)
        {
            var da = new CosmeticEntities();
            var answer = !da.Type.Any(m => m.CosmeticType == CosmeticType);
            return Json(answer, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string typeName)
        {
            try
            {
                var da = new CosmeticEntities();
                var model = da.Type.FirstOrDefault(m => m.CosmeticType == typeName);
                if (model == null)
                {
                    //没有对应记录
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                //检查这个分类名称是否已经使用
                if(da.Info.Any(m=>m.CosmeticType== typeName))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                da.Type.Remove(model);
                da.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(false, JsonRequestBehavior.AllowGet); }
        }
    }
}