using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports
{
    class Provider:Entity
    {
         public Provider(int ID, string name, string streetAddress, string city, int zipCode, string state)
        {
            this.ID = ID;
            this.name = name;
            this.streetAddress = streetAddress;
            this.city = city;
            this.zipCode = zipCode;
            this.state = state;
        }
    }
}
