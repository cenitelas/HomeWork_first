namespace DL.Entities
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public partial class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UsersBooks> UsersBooks { get; set; }
        public string Email { get; set; }
    }
}