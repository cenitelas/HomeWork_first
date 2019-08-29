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
    public class UserRepository : IRepository<Users>
    {

        private Model1 db;

        public UserRepository(Model1 context)
        {
            this.db = context;
        }

        public void Create(Users item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            Users user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public Users Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<Users> GetAll()
        {
            return db.Users;
        }

        public IEnumerable<Users> Find(Func<Users, Boolean> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Update(Users item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
