using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.Main;
using CosmeticsWeb.Models.ViewModels.AdminCosmetic;
using CosmeticsWeb.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    /// <summary>
    /// 后台化妆品管理
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminCosmeticController : Controller
    {
        private CosmeticService _cosmeticService;
        public AdminCosmeticController()
        {
            _cosmeticService = new CosmeticService();
        }
        // GET: AdminCosmetic
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        /// <summary>
        /// 所有化妆品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int cosmeticsPerPage = 8, int currentPageNo = 0)
        {
            var service = new CosmeticService();
            //指定页记录集合
            var model = _cosmeticService.GetAllByPage(cosmeticsPerPage, currentPageNo);

            //化妆品的总数量

            int pageCount = _cosmeticService.GetPageCount(cosmeticsPerPage);
            //将实际的页数传入View
            ViewBag.pageCount = pageCount;
            ViewBag.currentPageNo = currentPageNo;
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
                //向数据库插入数据
                _cosmeticService.Create(model);

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
        /// 对新增商品名称进行检验，如果已经存在返回false
        /// </summary>
        /// <param name="商品名称"></param>
        /// <returns></returns>
        public ActionResult RemoteValidateForNewCosmeticName(string 商品名称)
        {

            var answer = _cosmeticService.ValidateForNewCosmeticName(商品名称);
            return Json(answer, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 对已有类型名称进行检验，如果已经存在返回false
        /// </summary>
        /// <param name="商品名称"></param>
        /// <returns></returns>
        public ActionResult RemoteValidateForOldCosmeticName(string 商品名称, string 商品ID)
        {

            var answer = _cosmeticService.ValidateForOldCosmeticName(商品名称, 商品ID);
            return Json(answer, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 商品详细页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            return View(_cosmeticService.GetById(id));
        }

        /// <summary>
        /// 商品详细描述
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            return View(_cosmeticService.GetEditModelById(id));
        }

        [HttpPost]
        public ActionResult Edit(VmAdminCosmeticEdit model)
        {
            try
            {
                //输入校验
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //向数据库插入数据
                _cosmeticService.Edit(model);
                //转向
                return RedirectToAction("Detail", new { id = model.商品ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        /// <summary>
        /// 图片上传处理程序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage()
        {
            //商品id
            string id = Request.Form["id"];

            if (Request.Files.Count > 0)
            {
                //有上传文件
                var file = Request.Files[0];
                if (file != null)
                {
                    //文件扩展名
                    var ext = Path.GetExtension(file.FileName);

                    //存储的文件名
                    var newFileName = Server.MapPath("/images") + "\\" + id + ext;

                    //如果文件存在则删除
                    if (System.IO.File.Exists(newFileName))
                        System.IO.File.Delete(newFileName);
                    file.SaveAs(newFileName);
                }
            }
            return RedirectToAction("Detail", new { id = id });
        }


        /// <summary>
        /// 编辑商品描述
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditMessage(string id)
        {
            var model = _cosmeticService.GetById(id);
            var newModel = new VmContentEdit()
            {
                Id = id,
                Content = model.商品描述
            };
            return View(newModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditMessage(VmContentEdit model)
        {
            _cosmeticService.EditMessage(model);
            return RedirectToAction("Detail", new { id = model.Id });
        }
    }
}


