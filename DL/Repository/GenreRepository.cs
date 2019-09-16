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
    public class GenreRepository : IRepository<Genre>
    {
        private Model1 db;

        public GenreRepository(Model1 context)
        {
            this.db = context;
        }

        public void Create(Genre item)
        {
            db.Genre.Add(item);
        }

        public void Delete(int id)
        {
            Genre genre = db.Genre.Find(id);
            if (genre != null)
                db.Genre.Remove(genre);
        }

        public Genre Get(int id)
        {
            return db.Genre.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return db.Genre;
        }

        public IEnumerable<Genre> Find(Func<Genre, Boolean> predicate)
        {
            return db.Genre.Where(predicate).ToList();
        }

        public void Update(Genre item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
