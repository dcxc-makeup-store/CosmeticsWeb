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
        private CosmeticsEntities _context;
        public AccountService()
        {
            _context = new CosmeticsEntities();
        }

        public bool IsNameUsed(string name)
        {
            return _context.User.Any(m => m.UserName == name);
        }

        public void Create(VmRegister model)
        {
            var newUser = new User()
            {
                Password = "000000"
            };
            Util.CopyObjectData(model, newUser);
          
            _context.Configuration.ValidateOnSaveEnabled = false;
            _context.User.Add(newUser);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public User CheckLogin(VmLogin model)
        {
            var uid = model.Uid;
            var pwd =model.Pwd;//散列
                                              //如果存在满足条件的记录，则返回第一条
                                              //如果不存在，返回null
                                              // Lamda表达式 Linq for sql
            var item = _context.User.FirstOrDefault(m => m.UserName == uid && m.Password == pwd);
            return item;
        }
    }
}