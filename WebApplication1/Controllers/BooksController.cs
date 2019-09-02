using AutoMapper;
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

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        IBookService bookService;
        IAuthorService authorService;
        public BooksController(IBookService serv, IAuthorService serv2)
        {
            bookService = serv;
            authorService = serv2;
        }

        public ActionResult Index()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BBook, BookModel>()).CreateMapper();
            return View(mapper.Map<IEnumerable<BBook>, List<BookModel>>(bookService.GetBooks()));
        }


        // GET: Books/Create
        public ActionResult CreateAndEdit(int? id)
        {
            BookModel book = new BookModel();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BAuthor, AuthorModel>()).CreateMapper();
            List<AuthorModel> authors =  mapper.Map<IEnumerable<BAuthor>, List<AuthorModel>>(authorService.GetAuthors());
            ViewBag.AuthorId = new SelectList(authors, "Id", "FirstName");
            if (id != null)
            {
                mapper = new MapperConfiguration(cfg => cfg.CreateMap<BBook, BookModel>()).CreateMapper();
                book = mapper.Map<BBook, BookModel>(bookService.GetBook(id));
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(BookModel book)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookModel, BBook>()).CreateMapper();
            BBook newBook = mapper.Map<BookModel, BBook>(book);
            bookService.CreateOrUpdate(newBook);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            bookService.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
