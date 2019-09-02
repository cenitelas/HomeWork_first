﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BModel
{
    public class BBook
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }

        public int? Pages { get; set; }

        public int? Price { get; set; }
    }
}
