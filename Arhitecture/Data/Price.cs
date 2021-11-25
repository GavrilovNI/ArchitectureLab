using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhitecture.Data
{
    public class Price
    {
        private int _amount;
        
        public Price(int _amount)
        {
            throw new NotImplementedException();
        }

        public static implicit operator int(Price price)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Price(int price)
        {
            throw new NotImplementedException();
        }
    }
}
