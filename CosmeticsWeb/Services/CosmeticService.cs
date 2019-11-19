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

    }
}