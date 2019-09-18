using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public int Votes { get; set; }
    }
}