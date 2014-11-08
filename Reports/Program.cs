using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports
{
    class Program
    {
        static void Main(string[] args)
        {
            Report report = Report.getInstance;
            List<Member> memberList = new List<Member>();
            List<Provider> providerList = new List<Provider>();

            for (int i = 1; i <= 7; i++)
            {
                Member member = new Member(10000000 + i, "member name " + i, "University Dr", "Saint Cloud", 56301, "MN");
                memberList.Add(member);
            }
          //  report.createWeeklyMembersReport(memberList);
           

            for (int i = 1; i <= 7; i++)
            {
                Provider provider = new Provider(11111110 + i, "member name " + i, "University Dr", "Saint Cloud", 56301, "MN");
                providerList.Add(provider);
            }
            
            //report.createWeeklyProvidersReport(providerList);

            //Provider provider1 = new Provider(11111111, "member name ", "University Dr", "Saint Cloud", 56301, "MN");

            //report.getProvideReport(provider1);

            //report.getProviderSummary(providerList);

            report.createEFT(providerList);

            Console.ReadLine();
        }
    }
}
