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
using BL.BInterfaces;
using AutoMapper;
using BL.BModel;
using WebApplication1.Models;
using BL;
using BL.Utils;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    public class UsersBooksController : Controller
    {
        IUserBookService userBookService;
        IUserService userService;
        IBookService bookService;
        public UsersBooksController(IUserBookService serv, IUserService serv2, IBookService serv3)
        {
            userBookService = serv;
            userService = serv2;
            bookService = serv3;
        }

        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BUsersBook>,List<AuthorBook>>.Map(userBookService.GetUsersBooks));
        }

        public ActionResult CreateOrEdit(int? id=0)
        {
            ViewBag.date = DateTime.Now.ToString();
            List<BookModel> books = AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooks);
            List<UserModel> users = AutoMapper<IEnumerable<BUsers>, List<UserModel>>.Map(userService.GetUsers);

            if (id == null) { 
                ViewBag.books = new SelectList(books, "Id", "Title");          
                ViewBag.users = new SelectList(users, "Id", "Name");
                return View(new AuthorBook());
            }
            else
            {
                AuthorBook usersBooks = AutoMapper<BUsersBook, AuthorBook>.Map(userBookService.GetUserBook,(int)id);
                ViewBag.books = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.users = new SelectList(users, "Id", "Name", usersBooks.UserId);
                return View(usersBooks);
            }
        }

        [Logger]
        [HttpPost]
        public ActionResult CreateOrEdit(AuthorBook usersBooks)
        {
            List<BookModel> books = AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooks);
            List<UserModel> users = AutoMapper<IEnumerable<BUsers>, List<UserModel>>.Map(userService.GetUsers);

            if (usersBooks.DateOrder == null || usersBooks.DateOrder < DateTime.Now)
            {

                ViewBag.books = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.users = new SelectList(users, "Id", "Name", usersBooks.UserId);
                ViewBag.error = "Дата заказа не должна быть пустой и должна быть больше текущей даты";
                return View(usersBooks);
            }

            BUsersBook busersBooks = AutoMapper<AuthorBook,BUsersBook>.Map(usersBooks);

            if (userBookService.CheckUser(usersBooks.UserId))
            {
                userBookService.CreateOrUpdate(busersBooks);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Данный пользователь критический задолжник!!!!";
                ViewBag.BooksId = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.UserId = new SelectList(users, "Id", "Name", usersBooks.UserId);
                return View();
            }
        }

        [Logger]
        public ActionResult Delete(int id)
        {
            userBookService.DeleteUserBook(id);
            return RedirectToAction("Index");
        }

        [Logger]
        public ActionResult Download()
        {
            List<AuthorBook> dolj = AutoMapper<IEnumerable<BUsersBook>, List<AuthorBook>>.Map(userBookService.GetUsersBooks).Where(i => i.DateOrder < DateTime.Now).ToList();

            StringBuilder sb = new StringBuilder();
            string header = "#\tUser\tAuthor\tBook\tReturn";
            sb.Append(header);
            sb.Append("\r\n\r\n");
            sb.Append('-',header.Length*2);
            sb.Append("\r\n\r\n");
            foreach (var item in dolj)
            {
                sb.Append((dolj.IndexOf(item) + 1) + "\t" + item.UserName + "\t" + item.AuthorName + "\t" + item.BooksName+"\t"+item.DateOrder.Date+"\r\n");
            }
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());

            string contentType = "text/plain";
            return File(data, contentType, "users.txt");
        }

        [Logger]
        public ActionResult Link(int id)
        {
            BUsers user = userService.GetUser(id);
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
    }
}
