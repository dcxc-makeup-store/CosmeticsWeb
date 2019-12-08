using CosmeticsWeb.Models.EF;
using CosmeticsWeb.Models.Main;
using CosmeticsWeb.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmeticsWeb.Services
{
    public class AccountService
    {
        private CosmeticEntities _context;
        public AccountService()
        {
            _context = new CosmeticEntities();
        }

        public bool IsNameUsed(string name)
        {
            return _context.User.Any(m => m.UserName == name);
        }

        public void Create(VmRegiste model)
        {
            var newUser = new User();
            {
                Guid UserID = System.Guid.NewGuid();
            };
            Util.CopyObjectData(model, newUser);
            newUser.Password = Util.Md5Hash(newUser.Password);

            _context.User.Add(newUser);
            _context.SaveChanges();
        }
        public User CheckLogin(VmLogin model)
        {
            var uid = model.Uid;
            var pwd = Util.Md5Hash(model.Pwd);//散列
                //如果存在满足条件的记录，则返回第一条
            //如果不存在，返回null
            // Lamda表达式 Linq for sql
            var item = _context.User.FirstOrDefault(m => m.UserName == uid && m.Password == pwd);
            return item;
        }
    }
}