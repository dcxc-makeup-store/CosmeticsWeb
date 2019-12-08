﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Models.ViewModels.AdminCosmetic
{
    public class VmAdminCosmeticEdit
    {
        [Required(ErrorMessage = "必填项")]
        public System.Guid CosmeticID { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制50")]
        [Remote("RemoteValidateForNewCosmeticName", "AdminCosmetic", ErrorMessage = "化妆品名已经存在")]
        public string CosmeticName { get; set; }

        [Required(ErrorMessage = "必填项")]
        [UIHint("ViewCosmeticTypeSelector")]
        public string CosmeticType { get; set; }

        [Required(ErrorMessage = "必填项")]
        [Range(1, 10000, ErrorMessage = "限定范围1-10000")]
        [DataType(DataType.Currency, ErrorMessage = "必须是金额数字")]
        public decimal Price { get; set; }
       
        public int Stock { get; set; }
        
        public string Specification { get; set; }
   
    }
}