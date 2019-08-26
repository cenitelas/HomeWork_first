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
    public class UsersBooksController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Index()
        {
            var usersBooks = db.UsersBooks.Include(u => u.book).Include(u => u.user);
            return View(usersBooks.ToList());
        }

        public ActionResult CreateOrEdit(int? id)
        {
            if (id == null)
            {
                ViewBag.BooksId = new SelectList(db.Books, "Id", "Title");
                ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
                return View();
            }
            else
            {
                UsersBooks usersBooks = db.UsersBooks.Find(id);
                ViewBag.BooksId = new SelectList(db.Books, "Id", "Title", usersBooks.BooksId);
                ViewBag.UserId = new SelectList(db.Users, "Id", "Name", usersBooks.UserId);
                return View(usersBooks);
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(UsersBooks usersBooks)
        {
            if (usersBooks.Id==0)
            {
                db.UsersBooks.Add(usersBooks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else{
                db.Entry(usersBooks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            UsersBooks usersBooks = db.UsersBooks.Find(id);
            db.UsersBooks.Remove(usersBooks);
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
