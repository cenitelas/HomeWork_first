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
    public class GenreController : Controller
    {
        IGenreService genreService;
        public GenreController(IGenreService serv)
        {
            genreService = serv;
        }
        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BGenre>, List<GenreModel>>.Map(genreService.GetGenres));
        }

        public ActionResult EditAndCreate(int? id = 0)
        {
            GenreModel genre = AutoMapper<BGenre, GenreModel>.Map(genreService.GetGenre, (int)id);
            return View(genre);

        }

        [Logger]
        [HttpPost]
        public ActionResult EditAndCreate(GenreModel genre)
        {
            BGenre oldGenre = AutoMapper<GenreModel, BGenre>.Map(genre);
            genreService.CreateOrUpdate(oldGenre);
            return RedirectToActionPermanent("Index", "Genre");
        }

        [Logger]
        public ActionResult Delete(int id)
        {
            genreService.DeleteGenre(id);
            return RedirectToAction("Index", "Genre");
        }
    }
}