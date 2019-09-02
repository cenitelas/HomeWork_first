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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsersBook, AuthorBook>()).CreateMapper();
            return View(mapper.Map<IEnumerable<BUsersBook>, List<AuthorBook>>(userBookService.GetUsersBooks()));
        }

        public ActionResult CreateOrEdit(int? id)
        {
            ViewBag.date = DateTime.Now.ToString();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BBook, BookModel>()).CreateMapper();
            List<BookModel> books = mapper.Map<IEnumerable<BBook>, List<BookModel>>(bookService.GetBooks());
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsers, UserModel>()).CreateMapper();
            List<UserModel> users = mapper.Map<IEnumerable<BUsers>, List<UserModel>>(userService.GetUsers());

            if (id == null) { 
                ViewBag.BooksId = new SelectList(books, "Id", "Title");          
                ViewBag.UserId = new SelectList(users, "Id", "Name");
                return View();
            }
            else
            {
                mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsersBook, AuthorBook>()).CreateMapper();
                AuthorBook usersBooks = mapper.Map<BUsersBook, AuthorBook>(userBookService.GetUserBook(id));
                ViewBag.BooksId = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.UserId = new SelectList(users, "Id", "Name", usersBooks.UserId);
                return View(usersBooks);
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(AuthorBook usersBooks)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BBook, BookModel>()).CreateMapper();
            List<BookModel> books = mapper.Map<IEnumerable<BBook>, List<BookModel>>(bookService.GetBooks());
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsers, UserModel>()).CreateMapper();
            List<UserModel> users = mapper.Map<IEnumerable<BUsers>, List<UserModel>>(userService.GetUsers());

            if (usersBooks.DateOrder == null || usersBooks.DateOrder < DateTime.Now)
            {

                ViewBag.BooksId = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.UserId = new SelectList(users, "Id", "Name", usersBooks.UserId);
                ViewBag.error = "Дата заказа не должна быть пустой и должна быть больше текущей даты";
                return View();
            }

            mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorBook, BUsersBook>()).CreateMapper();
            BUsersBook busersBooks = mapper.Map<AuthorBook, BUsersBook>(usersBooks);

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

        public ActionResult Delete(int id)
        {
            userBookService.DeleteUserBook(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsersBook, AuthorBook>()).CreateMapper();
            List<AuthorBook> dolj =  mapper.Map<IEnumerable<BUsersBook>, List<AuthorBook>>(userBookService.GetUsersBooks().Where(i=>i.DateOrder>DateTime.Now));

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
