using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CosmeticsWeb.Models.ViewModels.AdminLogin
{
    public class VmAdminLogin
    {
        [DisplayName("密码")]
        [Required(ErrorMessage="必填项")]
        [DataType(DataType.Password)]
        public string Password { get;set; }
    }
}