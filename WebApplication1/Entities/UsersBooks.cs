namespace WebApplication1
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public partial class UsersBooks
    {
        public int Id { get; set; }
        public int BooksId { get; set; }
        public Books book { get; set; }
        public int UserId { get; set; }
        public Users user { get; set; }
    }
}