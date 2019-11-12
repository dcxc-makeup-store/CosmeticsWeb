using CosmeticsWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmeticsWeb.Models.Main
{
    public class Util
    {
        /// <summary>
        /// 返回所有的化妆品类型
        /// </summary>
        /// <returns></returns>
        
        public static List<string> AllCosmeticTypes()
        {
            var da = new CosmeticsEntities();
            return da.商品类型表.Select(m => m.商品类型名称).ToList();

        }
    }
}