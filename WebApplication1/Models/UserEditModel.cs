using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserEditModel
    {
        public Users user;
        public IEnumerable<Books> books;
    }
}