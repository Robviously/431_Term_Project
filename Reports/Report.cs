using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Interfaces;
using System.Xml;
using System.Xml.Linq;
using System.IO;
namespace Reports
{
    class Report : IMemberReport, IProviderReport, ISummaryReport, I_EFTReport
    {
        private static Report _report = null;
        private static object lockThis = new object();
        string MEMBER_TYPE = "member_type";
        string PROVIDER_TYPE = "provider_type";
        XElement serviceDB;

        private Report()
        {
            serviceDB = XElement.Load("C:/Users/Ruhshod/Desktop/ServicesXML.xml");
        }

        public static Report getInstance
        {
            get
            {
                lock (lockThis)
                {
                    if (_report == null)
                        _report = new Report();
                    return _report;
                }
            }
        }
        
        public void getWeeklyMembersReport(List<Member> memberList)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek+1);
            DateTime enddate = DateTime.Today;
        
            foreach (var member in memberList)
            {
                var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("memberID").Value == member.ID.ToString());

             
                List<XElement> serviceList = new List<XElement>();
                serviceList = services.ToList();
                if (serviceList.Count > 0)
                {
                    MakeFile(member, serviceList, MEMBER_TYPE);
                    
                }
            }
        }

        public void getMemberReport(Member member)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime enddate = DateTime.Today;

            var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("memberID").Value == member.ID.ToString());


            List<XElement> serviceList = new List<XElement>();
            serviceList = services.ToList();
            if (serviceList.Count > 0)
            {
                MakeFile(member, serviceList, MEMBER_TYPE);

            }
        }

        public void getWeeklyProvidersReport(List<Provider> providerList)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime enddate = DateTime.Today;

            foreach (var provider in providerList)
            {
                var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("providerID").Value == provider.ID.ToString());


                List<XElement> serviceList = new List<XElement>();
                serviceList = services.ToList();
                if (serviceList.Count > 0)
                {
                    MakeFile(provider, serviceList, PROVIDER_TYPE);

                }
            }
        }

        public void getProvideReport(Provider provider)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime enddate = DateTime.Today;

            var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("providerID").Value == provider.ID.ToString());


            List<XElement> serviceList = new List<XElement>();
            serviceList = services.ToList();
            if (serviceList.Count > 0)
            {
                MakeFile(provider, serviceList, PROVIDER_TYPE);
            }
        }

        public void getProviderSummary(List<Provider> providerList)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime enddate = DateTime.Today;
            int totalFee = 0;
            int provCount = 0;
            StreamWriter writer = new StreamWriter("C://Users//Ruhshod//Desktop//ProviderSummaryReport.txt");


            writer.WriteLine("********** Provider Weekly Summary Report ************");
            writer.Write("Provider Name");
            writer.Write("Total number of consultations ");
            writer.WriteLine("Total fee ");
            writer.WriteLine("--------------------------------------------------------------");
            writer.WriteLine();
            foreach (var provider in providerList)
            {
                int fee = 0;
                var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("providerID").Value == provider.ID.ToString());


                List<XElement> serviceList = new List<XElement>();
                serviceList = services.ToList();
                if (serviceList.Count > 0)
                {
                    foreach (var service in serviceList)
                    {
                        fee += Convert.ToInt32(service.Element("serviceFee").Value);
                        totalFee += Convert.ToInt32(service.Element("serviceFee").Value);
                    }
                    writer.Write("   "+provider.ID);
                    writer.Write("   "+provider.name);
                    writer.Write("   "+serviceList.Count);
                    writer.Write("   $"+fee);
                    writer.WriteLine();
                    provCount ++;
                }
            }
            writer.WriteLine();
            writer.Write("Total number of providers: ");
            writer.WriteLine(provCount);
            writer.Write("Total fee to be paid: $");
            writer.WriteLine(totalFee);

            writer.Close();

        }

        public void createEFT(List<Provider> providerList) 
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime enddate = DateTime.Today;
            StreamWriter writer = new StreamWriter("C://Users//Ruhshod//Desktop//EFT Report.txt");


            writer.WriteLine("********** Provider EFT Report ************");
            writer.Write("Provider Name");
            writer.Write("Total Provider Bank Account ");
            writer.WriteLine("Total fee ");
            writer.WriteLine("--------------------------------------------------------------");
            writer.WriteLine();
            foreach (var provider in providerList)
            {
                int fee = 0;
                var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("providerID").Value == provider.ID.ToString());


                List<XElement> serviceList = new List<XElement>();
                serviceList = services.ToList();
                if (serviceList.Count > 0)
                {
                    foreach (var service in serviceList)
                    {
                        fee += Convert.ToInt32(service.Element("serviceFee").Value);
                    }
                    writer.Write("   " + provider.ID);
                    writer.Write("   " + provider.name);
                    writer.Write("   #bank account number");
                    writer.Write("   $" + fee);
                    writer.WriteLine();
                }
            }
            writer.WriteLine();
            writer.Close();
        }

        private void MakeFile(Entity entity, List<XElement> serviceList, string entityType)
        {
            if (entityType == MEMBER_TYPE)
            {
                StreamWriter writer = new StreamWriter("C://Users//Ruhshod//Desktop//MemberReport - " + entity.ID + ".txt");
                writer.WriteLine("********** Member Weekly Report ************");
                writer.WriteLine();
                writer.WriteLine(entity.ID);
                writer.WriteLine(entity.name);
                writer.WriteLine(entity.streetAddress);
                writer.WriteLine(entity.city);
                writer.WriteLine(entity.state);
                writer.WriteLine(entity.zipCode);
                writer.WriteLine();
                writer.Write("    " + "Service Date");
                writer.Write("    " + "Provider Name");
                writer.WriteLine("    " + "Service Fee");
                writer.WriteLine("-----------------------------------------------------");
                foreach (var service in serviceList)
                {
                    writer.Write("    " + service.Element("serviceDate").Value);
                    writer.Write("    " + service.Element("providerID").Value);
                    writer.Write("    $" + service.Element("serviceFee").Value);
                    writer.WriteLine();
                }
                writer.Close();
            }

            else if (entityType == PROVIDER_TYPE)
            {
                int totalFee = 0;
                StreamWriter writer = new StreamWriter("C://Users//Ruhshod//Desktop//ProviderReport - " + entity.ID + ".txt");
                writer.WriteLine("********** Provider Weekly Report ************");
                writer.WriteLine();
                writer.WriteLine(entity.ID);
                writer.WriteLine(entity.name);
                writer.WriteLine(entity.streetAddress);
                writer.WriteLine(entity.city);
                writer.WriteLine(entity.state);
                writer.WriteLine(entity.zipCode);
                writer.Write("    " + "Received Date");
                writer.Write("    " + "Service Date");
                writer.Write("    " + "Member ID");
                writer.Write("    " + "Service Code");
                writer.WriteLine("    " + "Service Fee");
                writer.WriteLine("-----------------------------------------------------");
                foreach (var service in serviceList)
                {

                     writer.Write("    " + service.Element("currentDate").Value);
                     writer.Write("    " + service.Element("serviceDate").Value);
                     writer.Write("    " + service.Element("memberID").Value);
                     //writer.Write("    " + service.Element("memberName").Value);
                     writer.Write("    " + service.Element("serviceCode").Value);
                     writer.Write("    $" + service.Element("serviceFee").Value);
                     totalFee += Convert.ToInt32(service.Element("serviceFee").Value);
                     writer.WriteLine();
                }
                writer.WriteLine();
                writer.Write("    " + "Total number of consultations: ");
                writer.WriteLine(serviceList.Count);
                writer.Write("    " + "Total service fee: $");
                writer.WriteLine(totalFee);
                writer.Close();
            }
        }
    }
}
