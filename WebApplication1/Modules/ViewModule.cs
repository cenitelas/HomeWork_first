using BL;
using BL.BInterfaces;
using BL.Services;
using Ninject.Modules;
using Ninject.Web.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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
            Bind<IVoteService>().To<VoteService>();
            Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
        }
    }
}