using BL;
using BL.BInterfaces;
using BL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Modules
{
    public class ViewModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthorService>().To<AuthorService>();
            Bind<IBookService>().To<BookService>();
            Bind<IUserBookService>().To<UserBookService>();
            Bind<IUserService>().To<UserService>();
            Bind<IGenreService>().To<GenreService>();
            Bind<ILogDetailService>().To<LogDetailService>();
        }
    }
}