using CosmeticsWeb.Models.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Controllers
{
    /// <summary>
    /// 显示购物车
    /// </summary>
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 页面右上角显示购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowMyCart()
        {
            if (Session["Cart"] == null)
                Session.Add("Cart", new Cart());
            return PartialView(Session["Cart"]);
        }

        /// <summary>
        /// 向购物车加明细
        /// </summary>
        /// <param name="cosmeticId"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddItem(System.Guid cosmeticId, int amount, string cosmeticName,decimal price)
        {
            var cart = Session["Cart"] as Cart;
            var item = cart.Detail.FirstOrDefault(m => m.CosmeticID == cosmeticId);
            if (item == null)
                cart.Detail.Add(new CartItem()
                {
                    CosmeticID = cosmeticId,
                    Amount = amount,
                    CosmeticName = cosmeticName,
                    Price=price
                });
            else
            {
                item.Amount += amount;
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 显示购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult CartDetail()
        {
            if (Session["Cart"] == null)
                Session.Add("Cart", new Cart());
            return View((Session["Cart"] as Cart).Detail);
        }

        public ActionResult DeleteItem(Guid cosmeticId)
        {
            try
            {
                var cart = Session["Cart"] as Cart;
                var item = cart.Detail.FirstOrDefault(m => m.CosmeticID == cosmeticId);
                if (item != null)
                    cart.Detail.RemoveAll(m => m.CosmeticID == cosmeticId);
                Response.Redirect("/Cart/CartDetail");
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(false, JsonRequestBehavior.AllowGet); }
        }
        /// <summary>
        /// 清空购物车
        /// </summary>
        public void Clear()
        {
            var cart = Session["Cart"] as Cart;
            cart.Detail.Clear();
        }

        /// <summary>
        /// 统计总额
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotalValue()
        {
            var cart = Session["Cart"] as Cart;
            return cart.Detail.Sum(m => m.Price * m.Amount);
        }
    }
}