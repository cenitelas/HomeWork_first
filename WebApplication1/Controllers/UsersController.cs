using AutoMapper;
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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsers, UserModel>()).CreateMapper();
            return View(mapper.Map<IEnumerable<BUsers>, List<UserModel>>(userService.GetUsers()));
        }

        public ActionResult HistoryBooks()
        {
            return PartialView();
        }

        public ActionResult CreateOrEdit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsersBook, AuthorBook>()).CreateMapper();
                List<AuthorBook> ab =  mapper.Map<IEnumerable<BUsersBook>, List<AuthorBook>>(userService.GetReturnBooks((int)id));
                mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsers, UserModel>()).CreateMapper();
                UserModel user = mapper.Map<BUsers, UserModel>(userService.GetUser((int)id));
                ViewBag.books = ab;
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(UserModel model)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, BUsers>()).CreateMapper();
            BUsers user = mapper.Map<UserModel, BUsers>(model);
            userService.CreateOrUpdate(user);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
