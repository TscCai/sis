using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ncepu.GraduationProject.Sis.Persistence;

namespace Ncepu.GraduationProject.Sis.Web.Models.Account
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DbModel"].ConnectionString;

        // 只需重载此方法，模拟自定义的角色授权机制  
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string username;

            username = httpContext.User.Identity.Name;
            string group;
            if (httpContext.Session["Group"] == null)
            {
                DbModelDataContext db = new DbModelDataContext(conStr);
                try
                {
                    group = db.SIS_User.Single(u => u.Username == username).SIS_UserGroup.GroupName;
                }
                catch
                {
                    return false;
                }

            }
            else
            {
                group = httpContext.Session["Group"].ToString();
            }

            if (group == "admin")
            {
                return true;
            }
            return false;


        }
    }
}