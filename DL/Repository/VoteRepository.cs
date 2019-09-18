using DL.Entities;
using DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repository
{
    public class VoteRepository : IRepository<Vote>
    {

        private Model1 db;

        public VoteRepository(Model1 context)
        {
            this.db = context;
        }

        public void Create(Vote item)
        {
            db.Vote.Add(item);
        }

        public void Delete(int id)
        {
            Vote vote = db.Vote.Find(id);
            if (vote != null)
                db.Vote.Remove(vote);
        }

        public Vote Get(int id)
        {
            return db.Vote.Find(id);
        }

        public IEnumerable<Vote> GetAll()
        {
            return db.Vote;
        }

        public IEnumerable<Vote> Find(Func<Vote, Boolean> predicate)
        {
            return db.Vote.Where(predicate).ToList();
        }

        public void Update(Vote item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
