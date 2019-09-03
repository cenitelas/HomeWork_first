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
    public class UserBookService : IUserBookService
    {
        IUnitOfWork Database { get; set; }

        public UserBookService(IUnitOfWork uow)
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
                UsersBooks editUserBook = AutoMapper<BUsersBook, UsersBooks>.Map(userBook);
                Database.UsersBooks.Update(editUserBook);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BUsersBook GetUserBook(int id)
        {
            if (id != 0)
            {
                BUsersBook buserBook =  AutoMapper<UsersBooks, BUsersBook>.Map(Database.UsersBooks.Get,(int)id);
                buserBook.AuthorId = Database.Books.Get(buserBook.BooksId).AuthorId;
                buserBook.AuthorName = Database.Authors.Get(buserBook.AuthorId).FirstName;
                buserBook.BooksName = Database.Books.Get(buserBook.BooksId).Title;
                buserBook.UserName = Database.Users.Get(buserBook.UserId).Name;
                return buserBook;
            }
            return new BUsersBook();
        }

        public IEnumerable<BUsersBook> GetUsersBooks()
        {
            List<BUsersBook> buserBook = AutoMapper<IEnumerable<UsersBooks>, List<BUsersBook>>.Map(Database.UsersBooks.GetAll);
            for(int i = 0; i < buserBook.Count; i++)
            {
                buserBook[i] = GetUserBook(buserBook[i].Id);
            }
            return (IEnumerable<BUsersBook>) buserBook;
        }

        public bool CheckUser(int id)
        {
            UsersBooks usersBooks = Database.UsersBooks.Find(i => i.UserId == id && i.DateOrder <= DateTime.Now).FirstOrDefault();
            return (usersBooks == null) ? true : false;
        }

        public void DeleteUserBook(int id)
        {
            Database.UsersBooks.Delete(id);
            Database.Save();
        }

    }
}
