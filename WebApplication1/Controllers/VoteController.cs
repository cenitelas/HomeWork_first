using AutoMapper;
using BL;
using BL.BInterfaces;
using BL.BModel;
using BL.Services;
using BL.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VoteController : Controller
    {
        IVoteService voteService;
        IBookService bookService;
        public VoteController(IVoteService serv, IBookService serv2)
        {
            voteService = serv;
            bookService = serv2;
        }
        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BVote>, List<VoteModel>>.Map(voteService.GetVotes));
        }

        public ActionResult EditAndCreate(int? id = 0)
        {
            List<BookModel> books = AutoMapper<IEnumerable<BBook>, List<BookModel>>.Map(bookService.GetBooks);
            ViewBag.books = new SelectList(books, "Id", "Title", id);
            return View();

        }

        [Logger]
        [HttpPost]
        public ActionResult EditAndCreate(VoteModel vote)
        {
            BVote oldVote = AutoMapper<VoteModel, BVote>.Map(vote);
            voteService.CreateOrUpdate(oldVote);
            return RedirectToActionPermanent("Index", "Books");
        }

        [Logger]
        [HttpPost]
        public ActionResult VoteBook(int id = 0)
        {
            BVote vote = voteService.GetVote(id);
            vote.Votes += 1;
            voteService.CreateOrUpdate(vote);
            return PartialView(AutoMapper<IEnumerable<BVote>, List<VoteModel>>.Map(voteService.GetVotes));
        }
        public ActionResult Delete(int id)
        {
            voteService.DeleteVote(id);
            return RedirectToAction("Index", "Genre");
        }
    }
}