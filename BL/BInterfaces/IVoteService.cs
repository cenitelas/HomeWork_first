using BL.BModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BInterfaces
{
    public interface IVoteService
    {
        void CreateOrUpdate(BVote book);
        BVote GetVote(int id);
        IEnumerable<BVote> GetVotes();
        void DeleteVote(int id);
        void Dispose();
    }
}
