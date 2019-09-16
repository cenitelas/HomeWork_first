using DL.Entities;
using DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Model1 db;
        private UserRepository userRepository;
        private BooksRepository bookRepository;
        private AuthorRepository authorRepository;
        private UsersBooksRepository userBooksRepository;
        private GenreRepository genreRepository;
        private LogDetailRepository logDetailRepository;
        public UnitOfWork(string connection)
        {
            db = new Model1(connection);
        }

        public IRepository<Users> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<UsersBooks> UsersBooks
        {
            get
            {
                if (userBooksRepository == null)
                    userBooksRepository = new UsersBooksRepository(db);
                return userBooksRepository;
            }
        }

        public IRepository<Books> Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BooksRepository(db);
                return bookRepository;
            }
        }

        public IRepository<Authors> Authors
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(db);
                return authorRepository;
            }
        }

        public IRepository<Genre> Genre
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        public IRepository<LogDetail> LogDetails
        {
            get
            {
                if (logDetailRepository == null)
                    logDetailRepository = new LogDetailRepository(db);
                return logDetailRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
