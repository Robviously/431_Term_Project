using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Interfaces
{
    interface IMemberReport
    {
        List<string> getWeeklyMembersReport();
        string getMemberReport(int member);
    }
}