using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cart = CosmeticsWeb.Models.ViewModels.Cart.Cart;

namespace CosmeticsWeb.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        //页面右上角购物车
        public ActionResult ShowMyCart()
        {
            if (Session["Cart"] == null)
                Session.Add("Cart", new Cart());
            return PartialView(Session["Cart"]);
        }
        /// <summary>
        /// 向购物车加明细
        /// </summary>
   
        /// <param name="Amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddItem(System.Guid cosmeticId, int amount, string cosmeticName)
        {
            var cart = Session["Cart"] as Cart;
            var item = cart.Detail.FirstOrDefault(m => m.CosmeticID == cosmeticId);
            if (item == null)
                cart.Detail.Add(new CartItem()
                {
                    CosmeticID = cosmeticId,
                    Amount = amount,
                    CosmeticName = cosmeticName
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
    }
}