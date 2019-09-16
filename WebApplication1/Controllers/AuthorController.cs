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
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        IAuthorService authorService;
        static LogDetailService log;
        public AuthorController(IAuthorService serv, ILogDetailService serv2)
        {
            authorService = serv;
            log = (LogDetailService)serv2;
        }
        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BAuthor>, List<AuthorModel>>.Map(authorService.GetAuthors));
        }

        public ActionResult EditAndCreate(int? id=0)
        {
            AuthorModel author = AutoMapper<BAuthor, AuthorModel>.Map(authorService.GetAuthor,(int)id);
            return PartialView("EditOrCreate", author);

        }
        [Logger]
        [HttpPost]
        public ActionResult EditAndCreate(AuthorModel author)
        {
                BAuthor oldAuthor = AutoMapper<AuthorModel, BAuthor>.Map(author);
                authorService.CreateOrUpdate(oldAuthor);
                return PartialView("ViewAuthors", AutoMapper<IEnumerable<BAuthor>, List<AuthorModel>>.Map(authorService.GetAuthors));
   
        }
        [Logger]
        public ActionResult Delete(int id)
        {
            authorService.DeleteAuthor(id);
            return PartialView("ViewAuthors", AutoMapper<IEnumerable<BAuthor>, List<AuthorModel>>.Map(authorService.GetAuthors));
        }
    }
}