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
    public class OTPsRepository : RepositoryBase<OTPs>, IOTPsRepository
    {
        public OTPsRepository(RepositoryContext repositoryContext)
 : base(repositoryContext)
        {

        }
        public void CreateOtpObject(OTPs otp) => Create(otp);

        public OTPs GetOTPTimestampFromEmail(string RecipientEmail, bool trackChanges) =>
 FindByCondition(c => c.recipientEmail.Equals(RecipientEmail), trackChanges).OrderBy(c=>c.timestamp).Last();

    }
}
