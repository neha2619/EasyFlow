using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TimestampsRepository : RepositoryBase<Timestamps>, ITimestampsRepository
    {
        public TimestampsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public void InsertTimestamp(Timestamps timeStamps)=>Create(timeStamps);
        public IEnumerable<Timestamps> GetLastLoginTimeById(Guid recipientID, bool trackchanges) => FindByCondition(c=>c.RecipientID.Equals(recipientID),trackchanges).OrderBy(c => c.TimeStamp).ToList();
        public int GetTotalLogins(Guid id,bool trackChanges)=>FindByCondition(c=>c.RecipientID.Equals(id) , trackChanges).Count();
    }

}
