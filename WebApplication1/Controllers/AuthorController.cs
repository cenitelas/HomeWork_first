using AutoMapper;
using BL;
using BL.BInterfaces;
using BL.BModel;
using BL.Services;
using BL.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        IAuthorService authorService;
        public AuthorController(IAuthorService serv)
        {
            authorService = serv;
        }
        public ActionResult Index()
        {
            return View(AutoMapper<BAuthor, AuthorModel>.MapList(authorService.GetAuthors));
        }

        public ActionResult EditAndCreate(int? id=0)
        {
            AuthorModel author = AutoMapper<BAuthor, AuthorModel>.MapObject(authorService.GetAuthor,(int)id);
            return View(author);

        }

        [HttpPost]
        public ActionResult EditAndCreate(AuthorModel author)
        {
            BAuthor oldAuthor = AutoMapper<AuthorModel, BAuthor>.MapObject(author);
            authorService.CreateOrUpdate(oldAuthor);
            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {
            authorService.DeleteAuthor(id);
            return RedirectToAction("Index", "Author");
        }
    }
}