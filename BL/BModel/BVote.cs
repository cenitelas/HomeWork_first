using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BModel
{
    public class BVote
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int Votes { get; set; }
    }
}
