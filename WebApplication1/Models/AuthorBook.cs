using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AuthorBook
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int BooksId { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string BooksName { get; set; }

        public DateTime DateOrder { get; set; }
    }
}