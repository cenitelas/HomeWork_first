using BL.BModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BInterfaces
{
    public interface IUserBookService
    {
        void CreateOrUpdate(BUsersBook userBook);
        BUsersBook GetUserBook(int id);
        IEnumerable<BUsersBook> GetUsersBooks();
        void DeleteUserBook(int id);
        bool CheckUser(int id);
        void Dispose();
    }
}
