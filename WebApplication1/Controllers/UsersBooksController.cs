using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using System.IO;
using System.Text;

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
            ViewBag.date = DateTime.Now.ToString();
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
            if (usersBooks.DateOrder == null || usersBooks.DateOrder<DateTime.Now)
            {

                ViewBag.BooksId = new SelectList(db.Books, "Id", "Title", usersBooks.BooksId);
                ViewBag.UserId = new SelectList(db.Users, "Id", "Name", usersBooks.UserId);
                ViewBag.error = "Дата заказа не должна быть пустой и должна быть больше текущей даты";
                return View();
            }

            if (usersBooks.Id==0)
            {
                UsersBooks ub = db.UsersBooks.FirstOrDefault(i => i.UserId == usersBooks.UserId && i.DateOrder < DateTime.Now);
              
                    if (ub == null)
                    {
                        db.UsersBooks.Add(usersBooks);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Данный пользователь критический задолжник!!!!";
                        ViewBag.BooksId = new SelectList(db.Books, "Id", "Title", usersBooks.BooksId);
                        ViewBag.UserId = new SelectList(db.Users, "Id", "Name", usersBooks.UserId);

                        return View();
                    }
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

        public ActionResult Download()
        {
            List<UsersBooks> dolj = db.UsersBooks.Where(i => i.DateOrder < DateTime.Now).ToList();

            StringBuilder sb = new StringBuilder();
            string header = "#\tUser\tAuthor\tBook\tReturn";
            sb.Append(header);
            sb.Append("\r\n\r\n");
            sb.Append('-',header.Length*2);
            sb.Append("\r\n\r\n");
            foreach (var item in dolj)
            {
                var user = db.Users.Find(item.UserId);
                var book = db.Books.Find(item.BooksId);
                var author = db.Authors.Find(book.AuthorId);

                sb.Append((dolj.IndexOf(item) + 1) + "\t" + user.Name + "\t" + author.LastName + "\t" + book.Title+"\t"+item.DateOrder.Date+"\r\n");
            }
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());

            string contentType = "text/plain";
            return File(data, contentType, "users.txt");
        }

        public ActionResult Link(int id)
        {
            Users user = db.Users.Find(id);
            MailAddress from = new MailAddress("cenitelas@mail.ru", "RETURN MY BOOK!!!");
            // кому отправляем
            MailAddress to = new MailAddress(user.Email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "RETURN MY BOOK!!!";
            // текст письма - включаем в него ссылку
            m.Body = string.Format(user.Name+" верни книгу!!!!!");
            m.IsBodyHtml = true;
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new System.Net.NetworkCredential("cenitelas@mail.ru", "sanyaug777888");
            smtp.EnableSsl = true;
            smtp.Send(m);
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
