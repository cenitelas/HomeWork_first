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
        public VoteController(IVoteService serv)
        {
            voteService = serv;
        }
        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BVote>, List<VoteModel>>.Map(voteService.GetVotes));
        }

        public ActionResult EditAndCreate(int? id = 0)
        {
            VoteModel vote = AutoMapper<BVote, VoteModel>.Map(voteService.GetVote, (int)id);
            return View(vote);

        }

        [Logger]
        [HttpPost]
        public ActionResult EditAndCreate(VoteModel vote)
        {
            BVote oldVote = AutoMapper<VoteModel, BVote>.Map(vote);
            voteService.CreateOrUpdate(oldVote);
            return RedirectToActionPermanent("Index", "Vote");
        }

        [Logger]
        public ActionResult Delete(int id)
        {
            voteService.DeleteVote(id);
            return RedirectToAction("Index", "Genre");
        }
    }
}