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
        private CosmeticsService _cosmeticsService;

        public AdminCosmeticController()
        {
            _cosmeticsService = new CosmeticsService();
        }
        // GET: AdminCosmetic
        /*public ActionResult Index()
        {
            return RedirectToAction("List");
        }*/
        /// <summary>
        /// 所有化妆品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int cosmeticsPerPage=4,int currentPageNo=0)
        {
            //指定页记录集合
            var model = _cosmeticsService.GetAllByPage(cosmeticsPerPage, currentPageNo);

            //总数量
            int pageCount = _cosmeticsService.GetPageCount(cosmeticsPerPage);
            //将实际页数传入View
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
                //新增操作 向数据库插入数据
                var newModel = new Info();

                newModel.CosmeticID = Guid.NewGuid();
                // 将VmAdminBookCreate类型的model变量里的内容复制到newModel
                Util.CopyObjectData(model, newModel, "CosmeticID");
                //存入数据库
                var da = new CosmeticEntities();
                da.Info.Add(newModel);
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
        public ActionResult RemoteValidateForNewCosmeticName(string CosmeticName)
        {
            var da = new CosmeticEntities();
            var answer = !da.Info.Any(m => m.CosmeticName == CosmeticName);
            return Json(answer, JsonRequestBehavior.AllowGet);
        }
        //详细页
        public ActionResult Detail(System.Guid id)
        {
            var model = _cosmeticsService.GetById(id);
            return View(model);
        }
        public ActionResult Edit(System.Guid id)
        {
            return View(_cosmeticsService.GetEditModelById(id));
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
                _cosmeticsService.Edit(model);
                //转向
                return RedirectToAction("Detail", new { id = model.CosmeticID });
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
        /// 编辑目录页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCatalog(System.Guid id)
        {
            var model = _cosmeticsService.GetById(id);
            var newModel = new VmContentEdit()
            {
                Id = id,
                Content = model.Description
            };
            return View(newModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCatalog(VmContentEdit model)
        {
            _cosmeticsService.EditCatalog(model);
            return RedirectToAction("Detail", new { id = model.Id });
        }
    }
}