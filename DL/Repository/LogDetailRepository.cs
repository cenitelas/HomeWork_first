using DL.Entities;
using DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repository
{
    public class LogDetailRepository : IRepository<LogDetail>
    {

        private Model1 db;

        public LogDetailRepository(Model1 context)
        {
            this.db = context;
        }

        public void Create(LogDetail item)
        {
            db.LogDetails.Add(item);
        }

        public void Delete(int id)
        {
            LogDetail user = db.LogDetails.Find(id);
            if (user != null)
                db.LogDetails.Remove(user);
        }

        public LogDetail Get(int id)
        {
            return db.LogDetails.Find(id);
        }

        public IEnumerable<LogDetail> GetAll()
        {
            return db.LogDetails;
        }

        public IEnumerable<LogDetail> Find(Func<LogDetail, Boolean> predicate)
        {
            return db.LogDetails.Where(predicate).ToList();
        }

        public void Update(LogDetail item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
