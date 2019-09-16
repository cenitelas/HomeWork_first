using BL.Utils;
using BL.BInterfaces;
using BL.BModel;
using DL.Entities;
using DL.Interfaces;
using DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BookService:IBookService
    {
        IUnitOfWork Database { get; set; }

        public BookService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateOrUpdate(BBook book)
        {
            if (book.Id == 0)
            {

                Books dbook = new Books() { AuthorId=book.AuthorId, Pages=book.Pages, Price=book.Price, Title = book.Title, GenreId=book.GenreId};
                Database.Books.Create(dbook);
            }
            else
            {
                Books editBook = AutoMapper<BBook, Books>.Map(book);
                Database.Books.Update(editBook);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BBook GetBook(int id)
        {
            if (id != 0)
            {
                BBook book = AutoMapper<Books, BBook>.Map(Database.Books.Get,(int)id);
                book.AuthorName = Database.Authors.Get(book.AuthorId).FirstName;
                book.GenreName = Database.Genre.Get(book.GenreId).Name;
                return book;
            }
            return null;
        }

        public IEnumerable<BBook> GetBooks()
        {
            List<BBook> books =  AutoMapper<IEnumerable<Books>, List<BBook>>.Map(Database.Books.GetAll).ToList();
            books.ForEach(i => i.AuthorName = Database.Authors.Get(i.AuthorId).FirstName);
            books.ForEach(i => i.GenreName = Database.Genre.Get(i.GenreId).Name);
            return books;
        }

        public IEnumerable<BBook> GetBooksSortGenre(int id)
        {
            List<BBook> books = AutoMapper<IEnumerable<Books>, List<BBook>>.Map(Database.Books.Find(i=>i.GenreId==id)).ToList();
            books.ForEach(i => i.AuthorName = Database.Authors.Get(i.AuthorId).FirstName);
            books.ForEach(i => i.GenreName = Database.Genre.Get(i.GenreId).Name);
            return books;
        }

        public void DeleteBook(int id)
        {
            Database.Books.Delete(id);
            Database.Save();
        }

    }
}
