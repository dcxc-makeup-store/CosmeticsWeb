using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.Main;
using CosmeticsWeb.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmeticsWeb.Services
{
    /// <summary>
    /// 帐户相关服务
    /// </summary>
    public class AccountService
    {
        private CosmeticsEntities _context;
        public AccountService()
        {
            _context = new CosmeticsEntities();
        }

        public bool IsNameUsed(string name)
        {
            return _context.用户表.Any(m => m.用户名 == name);
        }

        public void Create(VmRegister model)
        {
            var newUser = new 用户表()
            {
                用户ID = Guid.NewGuid().ToString()
               
            };
        Util.CopyObjectData(model, newUser);
            _context.用户表.Add(newUser);
            _context.SaveChanges();
        }

}
}