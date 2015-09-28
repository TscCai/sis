using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ncepu.GraduationProject.Sis.Web.Models.Account;
using Ncepu.GraduationProject.Sis.Persistence;
using Ncepu.GraduationProject.Sis.Common;
using System.Web.Security;

namespace Ncepu.GraduationProject.Sis.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public AccountController()
        {
        }

        [HttpGet]
        public ActionResult LogOn()
        {
            ViewBag.Class = "alert-info";
            ViewBag.Hint = "请输入用户名和密码以登录。";
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(UserModel um)
        {
            bool success = false;
            using (DbModelDataContext db = DbContextFactory.Create())
            {
                SIS_User user = db.SIS_User.Where(u => u.Username == um.Username).First();
                if (user != null)
                {
                    string hashed = PasswordProvider.EncryptPassword(um.Password, user.Salt);
                    if (hashed == user.Password)
                    {
                        success = true;
                        System.Web.Security.FormsAuthentication.SetAuthCookie(user.Username, um.RememberMe);
                        if (Session["Group"] == null)
                        {
                            Session.Add("Group", user.SIS_UserGroup.GroupName);
                        }
                        else
                        {
                            Session["Group"] = user.SIS_UserGroup.GroupName;
                        }
                    }
                }
            }
            if (success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Class = "alert-error";
                ViewBag.Hint = "账号或密码错误！";
                return View();
            }

        }

        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }

        [Authorize]
        public ActionResult Settings()
        {
            DbModelDataContext db = DbContextFactory.Create();
            string username = HttpContext.User.Identity.Name;
            SIS_User user = db.SIS_User.Single(u => u.Username == username);
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ModifyPassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ModifyPassword(ModifyPasswordModel mpm)
        {
            DbModelDataContext db = DbContextFactory.Create();
            string username = HttpContext.User.Identity.Name;
            SIS_User user = db.SIS_User.Single(u => u.Username == username);
            string hashed = PasswordProvider.EncryptPassword(mpm.OriginPassword, user.Salt);
            if (mpm.NewPassword != mpm.ConfirmPassword)
            {
                ViewBag.Hint = "两次输入的密码不同。";
                ViewBag.Class = "alert alert-error";
                return View();
            }
            if (hashed != user.Password)
            {
                ViewBag.Hint = "原密码错误。";
                ViewBag.Class = "alert alert-error";
                return View();
            }
            else
            {
                string salt = PasswordProvider.GetSalt();
                user.Salt = salt;
                user.Password = PasswordProvider.EncryptPassword(mpm.NewPassword, salt);
                db.SubmitChanges();
                ViewBag.Hint = "密码已修改，请重新登陆。";
                ViewBag.Class = "alert alert-success";
                FormsAuthentication.SignOut();
                return View("LogOn");
            }

        }
    }
}
