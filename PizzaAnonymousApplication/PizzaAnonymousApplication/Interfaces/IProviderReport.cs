using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Interfaces
{
    interface IProviderReport
    {
        List<string> getWeeklyProvidersReport();
        string getProviderReport(int provider);
    }
}