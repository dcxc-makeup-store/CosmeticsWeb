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
            newUser.登录密码 = Util.StringToMD5Hash(newUser.登录密码);
            _context.用户表.Add(newUser);
            _context.SaveChanges();
        }
       
        public 用户表 CheckLogin(VmLogin model)
        {
            var uid = model.Uid;
            var pwd = Util.StringToMD5Hash(model.Pwd);//散列算法
            //如果存在满足条件的记录，则返回第一条
            //如果不存在，返回null
            //Lamda表达式
            var item = _context.用户表.FirstOrDefault(m=>m.用户名=uid m.登录密码=pwd);
            return item;
        }

    }
}