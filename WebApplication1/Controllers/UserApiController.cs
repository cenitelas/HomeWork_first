using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.Utils;
using BL.BModel;
using BL;
using WebApplication1.Models;
using BL.Utils;

namespace WebApplication1.Controllers
{
    public class UserApiController : ApiController
    {
        IUserService userService;

        public UserApiController(IUserService serv)
        {
            userService = serv;
        }

        public IEnumerable<UserModel> Get()
        {
            return AutoMapper<IEnumerable<BUsers>, List<UserModel>>.Map(userService.GetUsers);
        }

        
        public UserModel Get(int id)
        {
            return AutoMapper<BUsers, UserModel>.Map(userService.GetUser,id); ;
        }

        
        public void Post(UserModel value)
        {
            userService.CreateOrUpdate(AutoMapper<UserModel, BUsers>.Map(value));
        }

        
        public void Put(int id, UserModel value)
        {
            userService.CreateOrUpdate(AutoMapper<UserModel, BUsers>.Map(value));
        }

        
        public void Delete(int id)
        {
            userService.DeleteUser(id);
        }
    }
}