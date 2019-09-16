using BL.Utils;
using BL.BInterfaces;
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
using System.IO;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        IBookService bookService;
        IAuthorService authorService;
        IGenreService genreService;
        public BooksController(IBookService serv, IAuthorService serv2, IGenreService serv3)
        {
            bookService = serv;
            authorService = serv2;
            genreService = serv3;
        }

        public ActionResult Index()
        {
            List<GenreModel> genres = AutoMapper<IEnumerable<BGenre>, List<GenreModel>>.Map(genreService.GetGenres);
            GenreModel genre = new GenreModel() { Id = 0, Name = "All" };
            genres.Add(genre);
            ViewBag.genre = new SelectList(genres, "Id", "Name");
            return View(AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooks));
        }

        public ActionResult CreateAndEdit(int? id=0)
        {
            BookModel book = new BookModel();
            List<AuthorModel> authors = AutoMapper<IEnumerable<BAuthor>, List<AuthorModel>>.Map(authorService.GetAuthors);
            List<GenreModel> genres = AutoMapper<IEnumerable<BGenre>, List<GenreModel>>.Map(genreService.GetGenres);
            ViewBag.AuthorId = new SelectList(authors, "Id", "FirstName");
            ViewBag.GenreId = new SelectList(genres, "Id", "Name");
            if (id != 0)
            {
                book = AutoMapper<BBook, BookModel>.Map(bookService.GetBook,(int)id);
            }          
            return PartialView("CreateAndEdit", book);
        }

        [Logger]
        [HttpPost]
        public ActionResult CreateAndEdit(BookModel book, HttpPostedFileBase imageBook = null)
        {
            BBook newBook = AutoMapper<BookModel, BBook>.Map(book);
            byte[] imageData = null;
            if (imageBook != null)
            {
                using (var binaryReader = new BinaryReader(imageBook.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imageBook.ContentLength);
                }
                newBook.Image = imageData;
            }
            bookService.CreateOrUpdate(newBook);
            return PartialView("ViewBooks", AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooks));
        }

        [HttpPost]
        public ActionResult SortGenre(int? id=0)
        {
            if (id == 0)
                return PartialView("ViewBooks", AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooks));
            else
                return PartialView("ViewBooks", AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooksSortGenre,(int)id));

        }

        [Logger]
        public ActionResult Delete(int id)
        {
            bookService.DeleteBook(id);
            return PartialView("ViewBooks", AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooks));
        }
    }
}
