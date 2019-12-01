using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsWeb.Models.ViewModels.Account
{
    public class VmRegister
    {
      
        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        [Remote("RemoteValidateIsNameUsed", "Account", ErrorMessage = "用户名已经存在")]
        public string 用户名 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        [DataType(DataType.Password)]
        public string 登录密码 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        [System.ComponentModel.DataAnnotations.Compare("登录密码", ErrorMessage = "两次密码不同")]
        [DataType(DataType.Password)]
        public string 确认密码 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        [RegularExpression(@"^1([38][0-9]|4[579]|5[0-3,5-9]|6[6]|7[0135678]|9[89])\d{8}$", ErrorMessage = "手机号格式错误")]
        public string 手机号 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        [RegularExpression(@"^\w+@[a-z0-9]+\.[a-z]{2,4}$", ErrorMessage = "邮箱格式错误")]
        public string 邮箱 { get; set; }

        [DataType(DataType.MultilineText)]
        public string 个人简介 { get; set; }
        
        public byte[] 头像 { get; set; }
        
        public System.DateTime 用户生日 { get; set; }

        [Required(ErrorMessage = "必填项")]
        [MaxLength(50, ErrorMessage = "长度限制为50")]
        [DataType(DataType.Password)]
        public string 重置密码 { get; set; }
    }
}