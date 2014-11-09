
﻿/*
 * Reports program is responsible to generate all kind of reports. It is a singleton
 * class, meaning it only needs to be created once through out the program.
 * 
 * The class implements various interfaces to make sure, it has all the required functions needed
 * 
 * PRE - REQUISITES:
 * Database file called ServicesXML.xml file must exist
 *  
 * Created By: Rukhshod Abdullaev
 * 11/08/2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Interfaces;
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

        /// <summary>
        /// Loads the database file into serviceDB variable once the program is initiated. The path is where 
        /// actual database file is located. 
        /// </summary>
        private Report()
        {
            try
            {
                serviceDB = XElement.Load("C://Users//Ruhshod//Desktop//ServicesXML.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //method to implement singleton pattern
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
        
        /// <summary>
        /// Creates Weekly report for all members who has received a service in the last 7 days
        /// Function gets LIST of member objects from pizza anonymous UI and CHECKS if any of the members received a service
        /// </summary>
        /// <param name="memberList"></param>
        public void getWeeklyMembersReport(List<Member> memberList)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;
        
            foreach (var member in memberList)
            {
                var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("memberID").Value == member.Id.ToString());

             
                List<XElement> serviceList = new List<XElement>();
                serviceList = services.ToList();

                //generate a report if a member received any service in the past week
                if (serviceList.Count > 0)
                {
                    MakeFile(member, null, serviceList, MEMBER_TYPE);
                    
                }
            }
        }

        /// <summary>
        /// Function is to create report for all the services a member has received.
        /// Member object of whom report should be generated, will be passed by Pizza Anonymous
        /// </summary>
        /// <param name="member"></param>
        public void getMemberReport(Member member)
        {
            var services = serviceDB.Descendants("service")
                               .Where(x => x.Element("memberID").Value == member.Id.ToString());


            List<XElement> serviceList = new List<XElement>();
            serviceList = services.ToList();
            if (serviceList.Count > 0)
            {
                MakeFile(member, null, serviceList, MEMBER_TYPE);

            }
        }

        /// <summary>
        /// Creates Weekly report for all providers who has given a service in the last  7 days
        /// Function gets list of provider objects from pizza anonymous UI and CHECKS if any of the providers has given a service
        /// </summary>
        /// <param name="providerList"></param>
        public void getWeeklyProvidersReport(List<Provider> providerList)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;

            foreach (var provider in providerList)
            {
                var services = serviceDB.Descendants("service")
                               .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate && ((DateTime)x.Element("serviceDate")) <= enddate)
                               .Where(x => x.Element("providerID").Value == provider.Id.ToString());


                List<XElement> serviceList = new List<XElement>();
                serviceList = services.ToList();
                if (serviceList.Count > 0)
                {
                    MakeFile(null, provider, serviceList, PROVIDER_TYPE);

                }
            }
        }

        /// <summary>
        /// Function is to create report for ALL the services a provider has given.
        /// Provider object of whom report should be generated, will be passed by Pizza Anonymous
        /// </summary>
        /// <param name="provider"></param>
        public void getProviderReport(Provider provider)
        {
            var services = serviceDB.Descendants("service")
                               .Where(x => x.Element("providerID").Value == provider.Id.ToString());


            List<XElement> serviceList = new List<XElement>();
            serviceList = services.ToList();
            if (serviceList.Count > 0)
            {
                MakeFile(null, provider, serviceList, PROVIDER_TYPE);
            }
        }

        /// <summary>
        /// Creates summary report for all the providers who has given a service in the last 7 days
        ///  Function accepts list of provider object that will be passed by Pizza Anonymous.
        /// The path for stream writer is where the summary will be saved to
        /// </summary>
        /// <param name="providerList"></param>
        public void getProviderSummary(List<Provider> providerList)
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;
            int totalFee = 0;
            int provCount = 0;
            string path = "C://Users//Ruhshod//Desktop//Provider Report Summary.txt";

            try
            {
                //this where file will be saved
                StreamWriter writer = new StreamWriter(path);

                writer.WriteLine("********** Provider Weekly Summary Report ****".PadRight(60, '*'));
                writer.WriteLine();
                writer.Write("Provider Name".PadRight(20));
                writer.Write("Total # of consultations".PadRight(30));
                writer.WriteLine("Total fee".PadRight(15));
                writer.WriteLine("----".PadRight(60, '-'));
                writer.WriteLine();
                foreach (var provider in providerList)
                {
                    int fee = 0;
                    var services = serviceDB.Descendants("service")
                        .Where(
                            x =>
                                ((DateTime) x.Element("serviceDate")) >= startdate &&
                                ((DateTime) x.Element("serviceDate")) <= enddate)
                        .Where(x => x.Element("providerID").Value == provider.Id.ToString());


                    List<XElement> serviceList = new List<XElement>();
                    serviceList = services.ToList();
                    if (serviceList.Count > 0)
                    {
                        foreach (var service in serviceList)
                        {
                            fee += Convert.ToInt32(service.Element("serviceFee").Value);
                            totalFee += Convert.ToInt32(service.Element("serviceFee").Value);
                        }
                        writer.Write(provider.Name.PadRight(20));
                        writer.Write(serviceList.Count.ToString().PadRight(30));
                        writer.Write(("$" + fee).PadRight(15));
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

                Console.WriteLine("Provider summary report is saved to: {0}", path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Function is to create EFT report of all providers who has given a service in the last 7 days
        /// Function accepts list of provider objects from calling function.
        /// If report creation is successfull, a path for created report file will be printed
        /// </summary>
        /// <param name="providerList"></param>
        public void createEFT(List<Provider> providerList) 
        {
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;
            string path = "C:/Users/Ruhshod/Desktop/EFT Report.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path);


                writer.WriteLine("********** Provider EFT Report **********".PadRight(90, '*'));
                writer.WriteLine();
                writer.Write("Provider ID".PadRight(20));
                writer.Write("Provider Name".PadRight(20));
                writer.Write("Provider Bank Account #".PadRight(30));
                writer.WriteLine("Total fee".PadRight(15));
                writer.WriteLine("---".PadRight(90, '-'));
                writer.WriteLine();

                foreach (var provider in providerList)
                {
                    int fee = 0;
                    var services = serviceDB.Descendants("service")
                        .Where(x =>((DateTime) x.Element("serviceDate")) >= startdate &&
                                   ((DateTime) x.Element("serviceDate")) <= enddate)
                        .Where(x => x.Element("providerID").Value == provider.Id.ToString());


                    List<XElement> serviceList = new List<XElement>();
                    serviceList = services.ToList();
                    if (serviceList.Count > 0)
                    {


                        foreach (var service in serviceList)
                        {
                            fee += Convert.ToInt32(service.Element("serviceFee").Value);
                        }
                        writer.Write(provider.Id.ToString().PadRight(20));
                        writer.Write(provider.Name.PadRight(20));
                        writer.Write("#bank account number".PadRight(30));
                        writer.Write(("$" + fee).PadRight(15));
                        writer.WriteLine();
                    }
                }
                writer.WriteLine();
                writer.Close();

                Console.WriteLine("EFT Report is saved to {0}", path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// This is helper function to generate either provider or member reports. It will be called by
        /// getWeeklyProvidersReport or getWeeklyMembersReport
        /// Entity could be either Member or Provider Object, serviceList is list of selected services 
        /// that needs to be printed. Depending on entity type (provider or member) a report will be created. 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="serviceList"></param>
        /// <param name="entityType"></param>
        private void MakeFile(Member member, Provider provider, List<XElement> serviceList, string entityType)
        {
            if (entityType == MEMBER_TYPE)
            {
                string path = "C://Users//Ruhshod//Desktop//MemberReport - " + member.Id + ".txt";
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("********** Member Weekly Report *".PadRight(50, '*'));
                writer.WriteLine();
                writer.WriteLine(member.Id);
                writer.WriteLine(member.Name);
                writer.WriteLine(member.StreetAddress);
                writer.WriteLine(member.City);
                writer.WriteLine(member.State);
                writer.WriteLine(member.ZipCode);
                writer.WriteLine();
                writer.WriteLine();
                writer.Write("Service Date".PadRight(20));
                writer.Write("Provider Name".PadRight(15));
                writer.WriteLine("Service Fee".PadRight(15));
                writer.WriteLine("-".PadRight(50, '-'));
                foreach (var service in serviceList)
                {
                    writer.Write(service.Element("serviceDate").Value.PadRight(20));
                    writer.Write(service.Element("providerID").Value.PadRight(15));
                    writer.Write(("$" + service.Element("serviceFee").Value).PadRight(15));
                    writer.WriteLine();
                }
                writer.Close();

                Console.WriteLine("Report for member {0} is saved to {1}", member.Id, path);
            }

            else if (entityType == PROVIDER_TYPE)
            {
                string path = "C://Users//Ruhshod//Desktop//Provider Report - " + provider.Id + ".txt";
                int totalFee = 0;
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("***************** Provider Weekly Report *****".PadRight(90, '*'));
                writer.WriteLine();
                writer.WriteLine(member.Id);
                writer.WriteLine(member.Name);
                writer.WriteLine(member.StreetAddress);
                writer.WriteLine(member.City);
                writer.WriteLine(member.State);
                writer.WriteLine(member.ZipCode);
                writer.WriteLine();
                writer.Write(("Received Date").PadRight(23));
                writer.Write(("Service Date").PadRight(20));
                writer.Write(("Member ID").PadRight(15));
                writer.Write(("Service Code").PadRight(15));
                writer.WriteLine(("Service Fee").PadRight(15));
                writer.WriteLine("--".PadRight(90, '-'));
                foreach (var service in serviceList)
                {

                    writer.Write(service.Element("currentDate").Value.PadRight(23));
                    writer.Write(service.Element("serviceDate").Value.PadRight(20));
                    writer.Write(service.Element("memberID").Value.PadRight(15));
                    writer.Write(service.Element("serviceCode").Value.PadRight(15));
                    writer.Write(("$ " + service.Element("serviceFee").Value).PadRight(15));
                     totalFee += Convert.ToInt32(service.Element("serviceFee").Value);
                     writer.WriteLine();
                }
                writer.WriteLine();
                writer.Write("    " + "Total number of consultations: "); writer.WriteLine(serviceList.Count);
                writer.Write("    " + "Total service fee: $");  writer.WriteLine(totalFee);
                writer.Close();

                Console.WriteLine("Report for provider {0} is saved to {1}", provider.Id, path);
            }
        }
    }
}
