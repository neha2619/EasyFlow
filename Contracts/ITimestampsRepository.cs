using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITimestampsRepository
    { 
        void InsertTimestamp(Timestamps timeStamps);
        IEnumerable<Timestamps> GetLastLoginTimeById(Guid recipientID, bool trackchanges);
        int GetTotalLogins(Guid id, bool trackChanges);
    }
}
