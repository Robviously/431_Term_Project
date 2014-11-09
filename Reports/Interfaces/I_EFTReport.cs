using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Interfaces
{
    interface I_EFTReport
    {
        void createEFT(List<Provider> providersList);
    }
}
