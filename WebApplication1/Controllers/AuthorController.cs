using AutoMapper;
using BL;
using BL.BInterfaces;
using BL.BModel;
using BL.Services;
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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BAuthor, AuthorModel>()).CreateMapper();
            return View(mapper.Map<IEnumerable<BAuthor>, List<AuthorModel>>(authorService.GetAuthors()));
        }

        public ActionResult EditAndCreate(int? id)
        {
            AuthorModel author = new AuthorModel();
            if (id != null) {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BAuthor, AuthorModel>()).CreateMapper();
                author = mapper.Map<BAuthor, AuthorModel>(authorService.GetAuthor(id));
            }

            return View(author);

        }

        [HttpPost]
        public ActionResult EditAndCreate(AuthorModel author)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorModel, BAuthor>()).CreateMapper();
            BAuthor oldAuthor = mapper.Map<AuthorModel, BAuthor>(author);
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