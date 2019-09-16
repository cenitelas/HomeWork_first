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
    public class LogDetailService : ILogDetailService
    {
        IUnitOfWork Database { get; set; }

        public LogDetailService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateOrUpdate(BLogDetail log)
        {
            if (log.Id == 0)
            {

                LogDetail dLog = new LogDetail() { Message = log.Message, ActionName= log.ActionName, ControllerName= log.ControllerName, Date = log.Date, StackTrace=log.StackTrace};
                Database.LogDetails.Create(dLog);
            }
            else
            {
                LogDetail editLogDetail = AutoMapper<BLogDetail, LogDetail>.Map(log);
                Database.LogDetails.Update(editLogDetail);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BLogDetail GetLog(int id)
        {
            if (id != 0)
            {
                return AutoMapper<LogDetail, BLogDetail>.Map(Database.LogDetails.Get, (int)id);
            }
            return new BLogDetail();
        }

        public IEnumerable<BLogDetail> GetLogs()
        {
            return AutoMapper<IEnumerable<LogDetail>, List<BLogDetail>>.Map(Database.LogDetails.GetAll);
        }

        public void DeleteLog(int id)
        {
            Database.LogDetails.Delete(id);
            Database.Save();
        }

    }
}

