using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BModel;
using DL.Repository;
using DL.Entities;
using BL.Utils;
using BL.BInterfaces;
using DL.Interfaces;

namespace BL.Services
{
    public class AuthorService : IAuthorService
    {
        IUnitOfWork Database { get; set; }

        public AuthorService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateOrUpdate(BAuthor author)
        {
            if (author.Id == 0)
            {

                Authors dauthor = new Authors() { FirstName = author.FirstName, LastName = author.LastName };
                Database.Authors.Create(dauthor);               
            }
            else
            {
                Authors editAuthor = AutoMapper<BAuthor, Authors>.Map(author);
                Database.Authors.Update(editAuthor);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BAuthor GetAuthor(int id)
        {
            if (id != 0)
            {
                return AutoMapper<Authors, BAuthor>.Map(Database.Authors.Get,(int)id);
            }
            return new BAuthor();
        }

        public IEnumerable<BAuthor> GetAuthors()
        {
            return AutoMapper<IEnumerable<Authors>, List<BAuthor>>.Map(Database.Authors.GetAll);
        }

        public void DeleteAuthor(int id)
        {
            Database.Authors.Delete(id);
            Database.Save();
        }

    }
}

