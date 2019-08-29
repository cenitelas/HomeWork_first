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
    public class BooksRepository : IRepository<Books>
    {

        private Model1 db;

        public BooksRepository(Model1 context)
        {
            this.db = context;
        }

        public void Create(Books item)
        {
            db.Books.Add(item);
        }

        public void Delete(int id)
        {
            Books book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }

        public Books Get(int id)
        {
            return db.Books.Find(id);
        }

        public IEnumerable<Books> GetAll()
        {
            return db.Books;
        }

        public IEnumerable<Books> Find(Func<Books, Boolean> predicate)
        {
            return db.Books.Where(predicate).ToList();
        }

        public void Update(Books item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
