using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CosmeticsWeb.Models.Main
{
    /// <summary>
    /// ASP.NET Forms身份验证
    ///  参考
    /// </summary>
    public class AspFormsAuthentication 
    {
        /// <summary>
        /// 设置验证令牌
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="roles">角色</param>
        /// <param name="rememberMe">如果为 <c>true</c> ，则记住令牌；否则，不记住。</param>
        public static void SetAuthenticationToken(string userName, string[] roles, bool rememberMe)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                 2,
                 userName,
                 DateTime.Now,
                 DateTime.Now.AddDays(1),
                 rememberMe,
                 string.Join(",", roles)
                 );
            HttpCookie cookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public string GetAuthenticationToken()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        /// <summary>
        /// 获得角色
        /// </summary>
        /// <returns>角色数组</returns>
        public string[] GetRoles()
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (httpCookie != null)
            {
                FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.Decrypt(
                    httpCookie.Value
                    );
                if (formsAuthenticationTicket != null)
                    return formsAuthenticationTicket.UserData.Split(',');
            }
            return null;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}