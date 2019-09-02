using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BModel;
using DL.Repository;
using DL.Entities;
using AutoMapper;
using BL.BInterfaces;

namespace BL.Services
{
    public class AuthorService : IAuthorService
    {
        UnitOfWork Database { get; set; }

        public AuthorService(UnitOfWork uow)
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
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BAuthor, Authors>()).CreateMapper();
                Authors editAuthor = mapper.Map<BAuthor, Authors>(author);
                Database.Authors.Update(editAuthor);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BAuthor GetAuthor(int? id)
        {
            if (id != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Authors, BAuthor>()).CreateMapper();
                return mapper.Map<Authors, BAuthor>(Database.Authors.Get((int)id));
            }
            return null;
        }

        public IEnumerable<BAuthor> GetAuthors()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Authors, BAuthor>()).CreateMapper();
            return mapper.Map<IEnumerable<Authors>, List<BAuthor>>(Database.Authors.GetAll());
        }

        public void DeleteAuthor(int id)
        {
            Database.Authors.Delete(id);
            Database.Save();
        }

    }
}

