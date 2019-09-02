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
            return View(AutoMapper<BBook,BookModel>.MapList(bookService.GetBooks));
        }

        public ActionResult CreateAndEdit(int? id=0)
        {
            BookModel book = new BookModel();
            List<AuthorModel> authors = AutoMapper<BAuthor, AuthorModel>.MapList(authorService.GetAuthors).ToList();
            ViewBag.AuthorId = new SelectList(authors, "Id", "FirstName");
            if (id != 0)
            {
                book = AutoMapper<BBook, BookModel>.MapObject(bookService.GetBook,(int)id);
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(BookModel book)
        {
            BBook newBook = AutoMapper<BookModel, BBook>.MapObject(book);
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
