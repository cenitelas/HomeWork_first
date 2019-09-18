using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Books Book { get; set; }
        public int Votes { get; set; }
    }
}
