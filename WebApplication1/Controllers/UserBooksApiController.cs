using BL.BInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using BL.Utils;
using BL.BModel;

namespace WebApplication1.Controllers
{
    public class UserBooksApiController : ApiController
    {
        IUserBookService userBookService;

        public UserBooksApiController(IUserBookService serv)
        {
            userBookService = serv;
        }
        public IEnumerable<AuthorBook> Get()
        {
            return AutoMapper<IEnumerable<BUsersBook>, List<AuthorBook>>.Map(userBookService.GetUsersBooks);
        }

        public AuthorBook Get(int id)
        {
            return AutoMapper<BUsersBook, AuthorBook>.Map(userBookService.GetUserBook, id);
        }

        public void Post(AuthorBook value)
        {
            userBookService.CreateOrUpdate(AutoMapper<AuthorBook, BUsersBook>.Map(value));
        }

        public void Put(int id, AuthorBook value)
        {
            userBookService.CreateOrUpdate(AutoMapper<AuthorBook, BUsersBook>.Map(value));
        }

        public void Delete(int id)
        {
            userBookService.DeleteUserBook(id);
        }
    }
}
