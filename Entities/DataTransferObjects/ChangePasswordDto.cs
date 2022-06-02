using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ChangePasswordDto
    {
        
        public string password { get; set; }
        public string  confirmPassword { get; set; }
    }
}
