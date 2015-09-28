using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ncepu.GraduationProject.Sis.Web.Models.Account;
using Ncepu.GraduationProject.Sis.Persistence;
using Ncepu.GraduationProject.Sis.Common;

namespace Ncepu.GraduationProject.Sis.Web.Controllers.Admin
{
    [AdminAuthorize]
    public class UserController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            DbModelDataContext db = DbContextFactory.Create();
            IQueryable<SIS_User> users = db.SIS_User;
            return View(users);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Show = false;
            return View();
        }

        [HttpPost]
        public ActionResult Add(SIS_User user)
        {
            user.Salt = PasswordProvider.GetSalt();
            user.Password = PasswordProvider.EncryptPassword(user.Password, user.Salt);
            DbModelDataContext db = DbContextFactory.Create();
            if (db.SIS_User.Where(u => u.Username == user.Username).Count() == 0)
            {
                user.Status = "正常";
                user.GroupId = user.GroupId;
                db.SIS_User.InsertOnSubmit(user);
                db.SubmitChanges();
                ViewBag.Class = "alert-success";
                ViewBag.Hint = "用户添加成功！";
                ViewBag.Show = true;
                user = null;
                return View(user);
            }
            else
            {
                ViewBag.Class = "alert-error";
                ViewBag.Hint = "该用户名已被使用。";
                ViewBag.Show = true;
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            DbModelDataContext db = DbContextFactory.Create();
            SIS_User user = db.SIS_User.Single(u=>u.Id==id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(SIS_User user)
        {
            DbModelDataContext db=DbContextFactory.Create();
            try
            {
                SIS_User user_db = db.SIS_User.Single(u => u.Id == user.Id);
                user_db.GroupId = user.GroupId;
                user_db.Status = user.Status;
                db.SubmitChanges();
            }
            catch (InvalidOperationException)
            {
                return View("Error");
            }
            return RedirectToAction("Edit", new { id=user.Id });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            DbModelDataContext db = DbContextFactory.Create();
            try
            {
                SIS_User user = db.SIS_User.Single(u => u.Id == id);
                db.SIS_User.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
            catch(InvalidOperationException) 
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

    }
}
