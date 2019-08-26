using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Authors);
            return View(books.ToList());
        }


        // GET: Books/Create
        public ActionResult CreateAndEdit(int? id)
        {
            Books book = new Books();
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName");
            if (id != 0)
            {
                 book = db.Books.Find(id);
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(Books book)
        {
            if (book.Id != 0)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }else { 
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            Books books = db.Books.Find(id);
            db.Books.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
