using BL.BInterfaces;
using BL.BModel;
using BL.Utils;
using DL.Entities;
using DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class VoteService : IVoteService
    {
        IUnitOfWork Database { get; set; }

        public VoteService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateOrUpdate(BVote vote)
        {
            if (vote.Id == 0)
            {

                Vote dvote = new Vote() { BookId = vote.BookId, Votes = vote.Votes};
                Database.Vote.Create(dvote);
            }
            else
            {
                Vote editVote = AutoMapper<BVote, Vote>.Map(vote);
                Database.Vote.Update(editVote);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BVote GetVote(int id)
        {
            if (id != 0)
            {
                BVote vote = AutoMapper<Vote, BVote>.Map(Database.Vote.Get, (int)id);
                vote.BookName = Database.Books.Get(vote.BookId).Title;
                return vote;
            }
            return new BVote();
        }

        public IEnumerable<BVote> GetVotes()
        {
            List<BVote> votes = AutoMapper<IEnumerable<Vote>, List<BVote>>.Map(Database.Vote.GetAll);
            votes.ForEach(i=>i.BookName=Database.Books.Get(i.BookId).Title);
            return votes;
        }

        public void DeleteVote(int id)
        {
            Database.Vote.Delete(id);
            Database.Save();
        }

    }
}
