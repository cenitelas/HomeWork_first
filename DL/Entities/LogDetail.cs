using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class LogDetail
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; } 
        public string StackTrace { get; set; } 
        public DateTime Date { get; set; } 
    }
}
