using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhitecture.Data
{
    public class Password
    {
        private string _password;

        public Password(string password)
        {
            throw new NotImplementedException();
        }

        public static implicit operator string(Password password)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Password(string password)
        {
            throw new NotImplementedException();
        }
    }
}
