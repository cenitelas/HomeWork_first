using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Title { get; set; }

        public int? Pages { get; set; }

        public int? Price { get; set; }
    }
}