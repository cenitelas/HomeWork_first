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
    public class AuthorRepository : IRepository<Authors>
    {

        private Model1 db;

        public AuthorRepository(Model1 context)
        {
            this.db = context;
        }

        public void Create(Authors item)
        {
            db.Authors.Add(item);
        }

        public void Delete(int id)
        {
            Authors author = db.Authors.Find(id);
            if (author != null)
                db.Authors.Remove(author);
        }

        public Authors Get(int id)
        {
            return db.Authors.Find(id);
        }

        public IEnumerable<Authors> GetAll()
        {
            List<Authors> z = db.Authors.ToList();
            return z;
        }

        public IEnumerable<Authors> Find(Func<Authors, Boolean> predicate)
        {
            return db.Authors.Where(predicate);
        }

        public void Update(Authors item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
