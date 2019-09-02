using BL.BModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BInterfaces
{
    public interface IBookService
    {
        void CreateOrUpdate(BBook book);
        BBook GetBook(int id);
        IEnumerable<BBook> GetBooks();
        void DeleteBook(int id);
        void Dispose();
    }
}
