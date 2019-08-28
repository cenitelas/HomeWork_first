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
        private Model1 db = new Model1();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
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
                List<Books> history = db.Books.ToList().FindAll(i => i.UsersBooks.Any(z => z.UserId == id));
                Users users = db.Users.Find(id);
                List<AuthorBook> ab = new List<AuthorBook>();
                foreach(var item in history)
                {
                    if(history.IndexOf(item)>history.Count - 5)
                    ab.Add(new AuthorBook() { AuthorName = item.Authors.FirstName, BookTitle = item.Title });
                }
                ViewBag.books = ab;
                return View(users);
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(Users model)
        {
            if (model.Id==0)
            {
                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
