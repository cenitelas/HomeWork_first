using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BModel;
using DL.Repository;
using DL.Entities;
using DL.Interfaces;
using BL.Utils;

namespace BL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateOrUpdate(BUsers user)
        {
            if (user.Id==0)
            {

                Users duser = new Users() { Name = user.Name, Email = user.Email };
                Database.Users.Create(duser);
            }else
            {
                Users editUser = AutoMapper<BUsers, Users>.Map(user);
                Database.Users.Update(editUser);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BUsers GetUser(int id)
        {
            if (id != 0)
            {
                return AutoMapper<Users, BUsers>.Map(Database.Users.Get,(int)id);
            }
            return new BUsers();
        }

        public List<BUsersBook> GetReturnBooks(int id)
        {
            List<UsersBooks> ub = Database.UsersBooks.Find(i => i.UserId == id).ToList();
            List<BUsersBook> bub = new List<BUsersBook>();
            
            foreach(var item in ub)
            {
                Books book = Database.Books.Get(item.BooksId);
                Authors author = Database.Authors.Get(book.AuthorId);
                BUsersBook userBook = new BUsersBook() { Id=item.Id, AuthorId=author.Id, AuthorName=author.FirstName, BooksId=book.Id, BooksName=book.Title, UserId=item.UserId, UserName=Database.Users.Get(item.UserId).Name, DateOrder=item.DateOrder };
                bub.Add(userBook);
            }

            return bub; 
        }

        public IEnumerable<BUsers> GetUsers()
        {
            return AutoMapper<IEnumerable<Users>, List<BUsers>>.Map(Database.Users.GetAll);
        }

        public void DeleteUser(int id)
        {
            Database.Users.Delete(id);
            Database.Save();
        }

    }
}
