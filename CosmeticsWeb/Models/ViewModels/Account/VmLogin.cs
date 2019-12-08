using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CosmeticsWeb.Models.ViewModels.Account
{
    public class VmLogin
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "必填项")]
        [Display(Name ="登录名")]
        public string Uid { get; set; }
        
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "必填项")]
        [Display(Name = "登录密码")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
    }
}