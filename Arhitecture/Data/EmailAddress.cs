using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhitecture.Data
{
    public class EmailAddress
    {
        private string _address;

        public EmailAddress(string address)
        {
            throw new NotImplementedException();
        }

        public static implicit operator string(EmailAddress address)
        {
            throw new NotImplementedException();
        }

        public static explicit operator EmailAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
