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
    public class UsersBooksRepository : IRepository<UsersBooks>
    {

        private Model1 db;

        public UsersBooksRepository(Model1 context)
        {
            this.db = context;
        }

        public void Create(UsersBooks item)
        {
            db.UsersBooks.Add(item);
        }

        public void Delete(int id)
        {
            UsersBooks userBooks = db.UsersBooks.Find(id);
            if (userBooks != null)
                db.UsersBooks.Remove(userBooks);
        }

        public UsersBooks Get(int id)
        {
            return db.UsersBooks.Find(id);
        }

        public IEnumerable<UsersBooks> GetAll()
        {
            return db.UsersBooks;
        }

        public IEnumerable<UsersBooks> Find(Func<UsersBooks, Boolean> predicate)
        {
            return db.UsersBooks.Where(predicate).ToList();
        }

        public void Update(UsersBooks item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
