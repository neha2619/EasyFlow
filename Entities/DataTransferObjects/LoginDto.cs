using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Pass { get; set; }
    }
}
