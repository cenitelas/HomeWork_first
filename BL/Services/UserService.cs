using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BModel;
using DL.Repository;
using DL.Entities;
using AutoMapper;

namespace BL.Services
{
    public class UserService : IUserService
    {
        UnitOfWork Database { get; set; }

        public UserService(UnitOfWork uow)
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
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BUsers, Users>()).CreateMapper();
                Users editUser = mapper.Map<BUsers, Users>(user);
                Database.Users.Update(editUser);
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BUsers GetUser(int? id)
        {
            if (id != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, BUsers>()).CreateMapper();
                return mapper.Map<Users, BUsers>(Database.Users.Get((int)id));
            }
            return null;
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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, BUsers>()).CreateMapper();
            return mapper.Map<IEnumerable<Users>, List<BUsers>>(Database.Users.GetAll());
        }

        public void DeleteUser(int id)
        {
            Database.Users.Delete(id);
        }

    }
}
