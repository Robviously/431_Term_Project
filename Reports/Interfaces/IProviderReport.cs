using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Interfaces
{
    interface IProviderReport
    {
        void getWeeklyProvidersReport(List<Provider> providerList);
        void getProvideReport(Provider provider);
    }
}
