using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmeticsWeb.Models.ViewModels.Cart
{
    //购物车模型
    public class Cart
    {
        public List<CartItem> Detail { get; set; }
        public Cart()
        {
            Detail = new List<CartItem>();
        }
    }
    public class CartItem
    {
        public System.Guid CosmeticID { get; set; }
        public int Amount { get; set; }
        /// <summary>
        /// 名称，有冗余，为了显示
        /// </summary>
        public string CosmeticName { get; set; }

    }
}