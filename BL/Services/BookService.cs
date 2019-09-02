using AutoMapper;
using BL.BInterfaces;
using BL.BModel;
using DL.Entities;
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
        UnitOfWork Database { get; set; }

        public BookService(UnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateOrUpdate(BBook book)
        {
            if (book.Id == 0)
            {

                Books dbook = new Books() { AuthorId=book.AuthorId, Pages=book.Pages, Price=book.Price, Title = book.Title};
                Database.Books.Create(dbook);
            }
            else
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BBook, Books>()).CreateMapper();
                Books editBook = mapper.Map<BBook, Books>(book);
                Database.Books.Update(editBook);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BBook GetBook(int? id)
        {
            if (id != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Books, BBook>()).CreateMapper();
                BBook book = mapper.Map<Books, BBook>(Database.Books.Get((int)id));
                book.AuthorName = Database.Authors.Get(book.AuthorId).FirstName;
                return book;
            }
            return null;
        }

        public IEnumerable<BBook> GetBooks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Books, BBook>()).CreateMapper();
            List<BBook> books =  mapper.Map<IEnumerable<Books>, List<BBook>>(Database.Books.GetAll());
            books.ForEach(i => i.AuthorName = Database.Authors.Get(i.AuthorId).FirstName);
            return books;
        }

        public void DeleteBook(int id)
        {
            Database.Books.Delete(id);
            Database.Save();
        }

    }
}
