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
        public string 商品ID { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制50")]
        [Remote("RemoteValidateForNewCosmeticName", "AdminCosmetic", ErrorMessage = "化妆品名已经存在")]
        public string 商品名称 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [UIHint("ViewCosmeticTypeSelector")]
        public string 商品类型名称 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [Range(1, 10000, ErrorMessage = "限定范围1-10000")]
        [DataType(DataType.Currency, ErrorMessage = "必须是金额数字")]
        public decimal 商品单价 { get; set; }
        public int 商品库存 { get; set; }
        [Required(ErrorMessage = "必填项")]
        public string 商品规格 { get; set; }
   
    }
}