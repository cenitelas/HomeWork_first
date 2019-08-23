using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            List<Authors> authors;
            using (Model1 db = new Model1())
            {
                authors = db.Authors.ToList();

            }
            return View(authors) ;
        }

        public ActionResult EditAndCreate(int? id)
        {
            Authors author = new Authors();
            if (id != null)
            using (Model1 db = new Model1())
            {
                author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
            }
           
            return View(author);

        }

        [HttpPost]
        public ActionResult EditAndCreate(Authors author)
        {
            using (Model1 db = new Model1())
            {
                if (author.Id != 0)
                {
                    var oldAuthor = db.Authors.Where(a => a.Id == author.Id).FirstOrDefault();
                    oldAuthor.FirstName = author.FirstName;
                    oldAuthor.LastName = author.LastName;
                }
                else
                {
                    db.Authors.Add(author);
                }
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                var author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
                db.Authors.Remove(author);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Author");
        }
    }
}