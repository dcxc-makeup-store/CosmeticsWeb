using CosmeticsWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Models.ViewModels.Cart
{
    /// <summary>
    /// 购物车模型
    /// </summary>
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
        /// <summary>
        /// 商品Key
        /// </summary>
        public Guid CosmeticID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 名称，有冗余，为了显示
        /// </summary>
        public string CosmeticName { get; set; }

        public decimal Price { get; set; }

    }

}
/*@model IEnumerable<CosmeticsWeb.Models.ViewModels.Cart.CartItem>

@{
    ViewBag.Title = "CartDetail";
}

<h2>CartDetail</h2>



<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.CosmeticID)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Amount)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.CosmeticName)
        </th>
        <th>

        </th>
    </tr>

    @foreach (var item in Model)
    {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.CosmeticID)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Amount)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.CosmeticName)
            </td>
            <td>
                <button type="button" class="mya btn btn-primary" >删除购物车</button>

            </td>
        </tr>
    }

</table>

<style>
    .mya {
        color: red
    }
</style>

@section Scripts{
    <script>
    //浏览器端执行 与服务器端无关
        $(document).ready(function () {
            $(".mya").click(function () {//所有class等于mya的元素被点击的时候执行下端操作
                if (!confirm("是否确认操作?"))
                    return;
                //todelete  
                var amount = '@Model.Amount';
                var cosmeticId = '@Model.CosmeticID';
                var cosmeticName = '@Model.CosmeticName';
                var data = {
                    amount,
                    cosmeticId,
                    cosmeticName
                }
                $.ajax({
                    url: "/Cart/DeleteItem",
                    type: "post",
                    data: data,
                    catch: false,
                    async: false,
                    success: function () {
                        //成功
                        $("#cartarea").unload("/cart/showMyCart");
                },
                    error: function () {
                        alert("删除购物车失败")
                    }
                });
            })
         
        })
    </script>
}*/