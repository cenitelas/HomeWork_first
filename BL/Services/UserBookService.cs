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
    public class UserBookService : IUserBookService
    {
        UnitOfWork Database { get; set; }

        public UserBookService(UnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateOrUpdate(BUsersBook userBook)
        {
            if (userBook.Id == 0)
            {

                UsersBooks duserBook = new UsersBooks() {BooksId=userBook.BooksId, UserId=userBook.UserId, DateOrder = userBook.DateOrder };
                Database.UsersBooks.Create(duserBook);
            }
            else
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsersBook, UsersBooks>()).CreateMapper();
                UsersBooks editUserBook = mapper.Map<BUsersBook, UsersBooks>(userBook);
                Database.UsersBooks.Update(editUserBook);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BUsersBook GetUserBook(int? id)
        {
            if (id != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersBooks, BUsersBook>()).CreateMapper();
                BUsersBook buserBook =  mapper.Map<UsersBooks, BUsersBook>(Database.UsersBooks.Get((int)id));
                buserBook.AuthorId = Database.Books.Get(buserBook.BooksId).AuthorId;
                buserBook.AuthorName = Database.Authors.Get(buserBook.AuthorId).FirstName;
                buserBook.BooksName = Database.Books.Get(buserBook.BooksId).Title;
                buserBook.UserName = Database.Users.Get(buserBook.UserId).Name;
                return buserBook;
            }
            return null;
        }

        public IEnumerable<BUsersBook> GetUsersBooks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersBooks, BUsersBook>()).CreateMapper();
            List<BUsersBook> buserBook = mapper.Map<IEnumerable<UsersBooks>, List<BUsersBook>>(Database.UsersBooks.GetAll());
            for(int i = 0; i < buserBook.Count; i++)
            {
                buserBook[i] = GetUserBook(buserBook[i].Id);
            }
            return buserBook;
        }

        public bool CheckUser(int id)
        {
            UsersBooks usersBooks = Database.UsersBooks.Find(i => i.UserId == id && i.DateOrder <= DateTime.Now).FirstOrDefault();
            return (usersBooks == null) ? false : true;
        }

        public void DeleteUserBook(int id)
        {
            Database.UsersBooks.Delete(id);
            Database.Save();
        }

    }
}
