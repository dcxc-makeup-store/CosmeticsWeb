using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Models.ViewModels.AdminType
{
    public class VmAdminTypeCreate
    {
        [Required(ErrorMessage="必填项")]
        [MaxLength(50,ErrorMessage ="长度限制50")]
        [Remote("RemoteValidateForNewType","AdminBookType",ErrorMessage="分类已经存在")]
        public string CosmeticType { get; set; }//显示层UI
    }
}