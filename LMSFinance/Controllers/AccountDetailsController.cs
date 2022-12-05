using LMSFinance.Migrations;
using LMSFinance.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

namespace LMSFinance.Controllers
{
    public class AccountDetailsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Email
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return HttpNotFound();
            }

            var userInfo = new List<ApplicationUser>();

            foreach (var user in db.Users)
            {
                if (user.UserName == User.Identity.GetUserName())
                {
                    userInfo.Add(user);
                }
            };

            UserModel UserModel = new UserModel();
            UserModel.Users = userInfo;

            return View(UserModel.Users);
        }
    }
}