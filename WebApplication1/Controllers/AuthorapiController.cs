using BL.BInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.Utils;
using BL.BModel;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthorapiController : ApiController
    {
        IAuthorService authorService;
        public AuthorapiController(IAuthorService serv)
        {
            authorService = serv;
        }
        public IEnumerable<AuthorModel> Get()
        {
            return AutoMapper<IEnumerable<BAuthor>, List<AuthorModel>>.Map(authorService.GetAuthors);
        }

        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody]string value)
        {
            var a = value;
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
