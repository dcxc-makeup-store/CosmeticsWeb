using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.Main;
using CosmeticsWeb.Models.ViewModels.AdminCosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmeticsWeb.Services
{
    /// <summary>
    /// 提供Cosmetic相关服务
    /// 与其他组件之间耦合度越小越好
    /// </summary>
    public class CosmeticService
    {
        private CosmeticsEntities _context;
        public CosmeticService()
        {
            _context = new CosmeticsEntities();
        }
        /// <summary>
        /// 返回所有记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<商品信息表> GetAll()
        {
            //根据分页要求，取当前页的记录集合
            var model = _context.商品信息表.OrderBy(m => m.商品ID);
            return model;
        }

        /// <summary>
        /// 返回指定数量的化妆品 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<商品信息表> GetAllByNumber(int number)
        {
            //根据分页要求，取当前页的记录集合
            var model = _context.商品信息表.OrderBy(m => m.商品ID).Take(number);
            return model;
        }

        /// <summary>
        /// 返回含指定名称的化妆品 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<商品信息表> GetAllSearchByName(string name)
        {
            //根据分页要求，取当前页的记录集合
            var model = _context.商品信息表.OrderBy(m => m.商品ID).Where(m => m.商品名称.Contains(name));
            return model;
        }

        /// <summary>
        /// 根据分页要求，去当前页的记录集合
        /// </summary>
        /// <param name="cosmeticsPerPage">每页记录数</param>
        /// <param name="currentPageNo">当前页数，从0开始</param>
        /// <returns>所求的单页数据</returns>
        public IEnumerable<商品信息表> GetAllByPage(int cosmeticsPerPage, int currentPageNo)
        {
            //根据分页要求，取当前页的记录集合
            var model = _context.商品信息表.OrderBy(m => m.商品ID)
                .Skip(cosmeticsPerPage * currentPageNo)
                .Take(cosmeticsPerPage);
            return model;
        }

        /// <summary>
        /// 返回总页数
        /// </summary>
        /// <param name="booksPerPage">每页记录数</param>
        /// <returns>总页数</returns>
        public int GetPageCount(int cosmeticsPerPage)
        {
            int countOfCosmetics = _context.商品信息表.Count();
            //计算页数
            double dblPageCount = countOfCosmetics / (cosmeticsPerPage*1.0);
            int pageCount = (int)Math.Ceiling(dblPageCount);
            return pageCount;
        }

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model"></param>
        public void Create(VmAdminCosmeticCreate model)
        {
            //向数据库插入数据
            var newModel = new 商品信息表();
            newModel.商品ID = Guid.NewGuid().ToString();
            // 将VmAdminCosmeticCreate类型的model变量里的内容复制到newModel
            Util.CopyObjectData(model, newModel, "商品ID");
            newModel.商品描述 = "";
            newModel.商品品牌 = "";
            _context.商品信息表.Add(newModel);
            _context.SaveChanges();
        }

        /// <summary>
        /// 新化妆品名校验（是否已经使用）
        /// </summary>
        /// <param name="newCosmeticName"></param>
        /// <returns></returns>
        public bool ValidateForNewCosmeticName(string newCosmeticName)
        {
            return !_context.商品信息表.Any(m => m.商品名称 == newCosmeticName);
        }

        /// <summary>
        /// 已有化妆品名校验（是否已经使用）
        /// </summary>
        /// <param name="newCosmeticName"></param>
        /// <returns></returns>
        public bool ValidateForOldCosmeticName(string newCosmeticName, string cosmeticId)
        {
            return !_context.商品信息表.Any(m => m.商品名称 == newCosmeticName && m.商品ID != cosmeticId);
        }

        public 商品信息表 GetById(string id)
        {
            return _context.商品信息表.FirstOrDefault(m => m.商品ID==id);
        }

        public VmAdminCosmeticEdit GetEditModelById(string id)
        {
            //从数据库中取出相应数据
            var model = _context.商品信息表.FirstOrDefault(m =>m.商品ID==id);
            //填充相应ViewModel
            var newModel = new VmAdminCosmeticEdit();
            Util.CopyObjectData(model, newModel);
            return newModel;
        }

        /// <summary>
        /// 保存修改信息
        /// </summary>
        /// <param name="model"></param>
        public void Edit(VmAdminCosmeticEdit model)
        {
            //向数据库插入数据
            var newModel = _context.商品信息表.FirstOrDefault(m => m.商品ID == model.商品ID);
            // 将VmAdminCosmeticEdit类型的model变量里的内容复制到newModel
            Util.CopyObjectData(model, newModel, "商品ID");

            _context.SaveChanges();
        }
        public void EditMessage(VmContentEdit model)
        {
            var item = _context.商品信息表.Find(model.Id);
            if (item != null)
            {
                item.商品描述 = model.Content;
                _context.SaveChanges();
            }
        }
    }
}