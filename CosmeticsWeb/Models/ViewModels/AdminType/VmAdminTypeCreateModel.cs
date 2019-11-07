using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Models.ViewModels.AdminType
{
    public class VmAdminTypeCreateModel
    {
        [Required(ErrorMessage="必填项")]
        [MaxLength(50,ErrorMessage ="长度限制50")]
        [Remote("RemoteValide","MyRemote",ErrorMessage="商品类型表")]
        public string 商品类型名称 { get; set; }
    }
}