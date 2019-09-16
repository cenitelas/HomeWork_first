using BL.Utils;
using BL;
using BL.BModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.Models;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        IUserService userService;
        public UsersController(IUserService serv)
        {
            userService = serv;
        }

        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BUsers>,List<UserModel>>.Map(userService.GetUsers));
        }

        public ActionResult HistoryBooks()
        {
            return PartialView();
        }

        public ActionResult CreateOrEdit(int? id=0)
        {
                List<AuthorBook> ab =  AutoMapper<IEnumerable<BUsersBook>, List<AuthorBook>>.Map(userService.GetReturnBooks,(int)id);
                UserModel user = AutoMapper<BUsers, UserModel>.Map(userService.GetUser,(int)id);
                ViewBag.books = ab;

                return View(user);
        }

        [Logger]
        [HttpPost]
        public ActionResult CreateOrEdit(UserModel model)
        {
            BUsers user = AutoMapper<UserModel, BUsers>.Map(model);
            userService.CreateOrUpdate(user);
            return RedirectToAction("Index");
        }

        [Logger]
        public ActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
