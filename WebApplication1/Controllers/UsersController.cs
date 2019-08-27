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

        public ActionResult HistoryBooks(UserEditModel model)
        {
           return PartialView(model.books);
        }

        public ActionResult CreateOrEdit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                IEnumerable<Books> history = db.Books.ToList().FindAll(i => i.UsersBooks.Any(z => z.UserId == id));

                Users users = db.Users.Find(id);
                UserEditModel userModel = new UserEditModel() { user = users, books = history };
                ViewBag.history = new SelectList(history);
                return View(userModel);
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(Users users)
        {
            if (users.Id==0)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(users).State = EntityState.Modified;
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
