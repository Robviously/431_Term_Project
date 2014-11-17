
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
 * Initial create date: 11/08/2014
  * 
  * Modified date: 11/15/2014
  * Modified by: Rukhshod Abdullaev
 */
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Reports.Interfaces;
using System.Xml.Linq;
using System.IO;
namespace PizzaAnonymousApplication
{
    public class Report : IMemberReport, IProviderReport, ISummaryReport, I_EFTReport
    {
        private static Report _report = null;             //to implement singleton pattern
        private static object lockThis = new object();    //multi thread support

        ///types of reports to be generated
        private readonly string MEMBER_WEEKLY = "Member Weekly Report";
        private readonly string PROVIDER_WEEKLY = "Provider Weekly Report";
        private readonly string MEMBER_ON_DEMAND = "Member Report";
        private readonly string PROVIDER_ON_DEMAND = "Provider Report";
        private readonly string PROVIDER_SUMMARY = "Provider Summary Report";
        private readonly string EFT_SUMMARY = "EFT Report";

        XElement serviceXML = null;

        /// <summary>
        /// Loads the database file from user's desktop into serviceDB variable once the program is initiated. The path is where 
        /// actual database file is located. 
        /// </summary>
        private Report()
        {
            try
            {
                Console.WriteLine(Environment.CurrentDirectory);
                serviceXML = XElement.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ServicesXML.xml does not exist in your desktop directory. Please create a ServicesXML.xml in your desktop directory");
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
        public void getWeeklyMembersReport()
        {
            //starting and ending date to work withs
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;

            if (!serviceXML.Descendants("service").Any())
                Console.WriteLine("Service database has no services captured");
            //ServicesXML.xml is not empty
            else
            {
                try
                {
                    //query to get list of members who has received a service in the last 7 days
                    var memberQuery = serviceXML.Descendants("service")
                        .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                                    ((DateTime)x.Element("serviceDate")) <= enddate);

                    //extract unique member ids and export to list
                    List<string> memberList = memberQuery.Select(i => i.Element("memberID").Value).Distinct().ToList();

                    if (memberList.Count > 0)
                    {
                        //make a report for each member in the list
                        foreach (var member in memberList)
                        {
                            //query to get list of services in the last 7 days for particular member ID
                            var serviceQuery = serviceXML.Descendants("service")
                                .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                                            ((DateTime)x.Element("serviceDate")) <= enddate)
                                .Where(x => x.Element("memberID").Value == member);

                            //export results of query to list
                            List<XElement> serviceList = serviceQuery.ToList();

                            //generate a report if a member received any service in the past week
                            string id = serviceList[0].Element("memberID").Value;
                            string name = serviceList[0].Element("memberName").Value;
                            string strtAddr = serviceList[0].Element("mStrtAddr").Value;
                            string city = serviceList[0].Element("mCity").Value;
                            string state = serviceList[0].Element("mState").Value;
                            string zip = serviceList[0].Element("mZip").Value;
                            //make a report .txt file
                            MakeFile(id, name, strtAddr, city, state, zip, serviceList, MEMBER_WEEKLY);

                        }
                    }
                    else
                    {
                        Console.WriteLine("None of the members has receieved a service in the past 7 days");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Function is to create report for all the services a member has received.
        /// Member object of whom report should be generated, will be passed by Pizza Anonymous
        /// </summary>
        /// <param name="member"></param>
        public void getMemberReport(int member)
        {
            //query to get list of services for particular member ID
            var serviceQuery = serviceXML.Descendants("service")
                   .Where(x => x.Element("memberID").Value == member.ToString());

            //export query result into list
            List<XElement> serviceList = serviceQuery.ToList();

            //generate a report if a member received any service in the past week
            if (serviceList.Count > 0)
            {
                string id = serviceList[0].Element("memberID").Value;
                string name = serviceList[0].Element("memberName").Value;
                string strtAddr = serviceList[0].Element("mStrtAddr").Value;
                string city = serviceList[0].Element("mCity").Value;
                string state = serviceList[0].Element("mState").Value;
                string zip = serviceList[0].Element("mZip").Value;

                //make a report .txt file
                MakeFile(id, name, strtAddr, city, state, zip, serviceList, MEMBER_ON_DEMAND);
            }
        }

        /// <summary>
        /// Creates Weekly report for all providers who has given a service in the last  7 days
        /// Function gets list of provider objects from pizza anonymous UI and CHECKS if any of the providers has given a service
        /// </summary>
        /// <param name="providerList"></param>
        public void getWeeklyProvidersReport()
        {
            //date range for the past 7 days
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;

            //query to get list of providers who has given a service in the last 7 days
            var provQuery = serviceXML.Descendants("service")
                .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                            ((DateTime)x.Element("serviceDate")) <= enddate);

            //export results into list, get only unique provider ids
            List<string> providerList = provQuery.Select(i => i.Element("providerID").Value).Distinct().ToList();

            //make a report for each provider in the list
            foreach (var provider in providerList)
            {
                //query to get list of services for particular provider in the last 7 days
                var serviceQuery = serviceXML.Descendants("service")
                    .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                               ((DateTime)x.Element("serviceDate")) <= enddate)
                    .Where(x => x.Element("providerID").Value == provider);


                //export query result into list
                List<XElement> serviceList = serviceQuery.ToList();

                //generate a report if a provider received any service in the past week
                if (serviceList.Count > 0)
                {
                    string id = serviceList[0].Element("providerID").Value;
                    string name = serviceList[0].Element("providerName").Value;
                    string strtAddr = serviceList[0].Element("pStrtAddr").Value;
                    string city = serviceList[0].Element("pCity").Value;
                    string state = serviceList[0].Element("pState").Value;
                    string zip = serviceList[0].Element("pZip").Value;

                    //make a report .txt file
                    MakeFile(id, name, strtAddr, city, state, zip, serviceList, PROVIDER_WEEKLY);
                }
            }
        }

        /// <summary>
        /// Function is to create report for ALL the services a provider has given.
        /// Provider object of whom report should be generated, will be passed by Pizza Anonymous
        /// </summary>
        /// <param name="provider"></param>
        public void getProviderReport(int provider)
        {
            //qeuery to get list of all services for particular provider
            var serviceQuery = serviceXML.Descendants("service")
                   .Where(x => x.Element("providerID").Value == provider.ToString());

            //export result into list
            List<XElement> serviceList = serviceQuery.ToList();

            //generate a report if a member received any service in the past week
            if (serviceList.Count > 0)
            {
                string id = serviceList[0].Element("providerID").Value;
                string name = serviceList[0].Element("providerName").Value;
                string strtAddr = serviceList[0].Element("pStrtAddr").Value;
                string city = serviceList[0].Element("pCity").Value;
                string state = serviceList[0].Element("pState").Value;
                string zip = serviceList[0].Element("pZip").Value;

                //make a report text file
                MakeFile(id, name, strtAddr, city, state, zip, serviceList, PROVIDER_ON_DEMAND);
            }
        }

        /// <summary>
        /// Creates summary report for all the providers who has given a service in the last 7 days
        ///  Function accepts list of provider object that will be passed by Pizza Anonymous.
        /// The path for stream writer is where the summary will be saved to
        /// </summary>
        /// <param name="providerList"></param>
        public void getProviderSummary()
        {
            //date range
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;

            int totalFee = 0;      //total fee for all providers
            int provCount = 0;
            int totalConsultCount = 0;
            //this is where report text file is created
            string path = Environment.CurrentDirectory + "/Reports/"+"/Provider Report Summary.txt";

            //query to get list of providers who has given a service in the last 7 days
            var provQuery = serviceXML.Descendants("service")
                .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                            ((DateTime)x.Element("serviceDate")) <= enddate);

            //export results into list, get only unique provider ids
            List<string> providerList = provQuery.Select(i => i.Element("providerID").Value).Distinct().ToList();
            StreamWriter writer = new StreamWriter(path);

            try
            {
                writer.WriteLine("********** Provider Weekly Summary Report ****".PadRight(60, '*'));
                writer.WriteLine();
                writer.Write("Provider Name".PadRight(20));
                writer.Write("Total # of consultations".PadRight(30));
                writer.WriteLine("Total fee".PadRight(15));
                writer.WriteLine("----".PadRight(60, '-'));
                writer.WriteLine();
                foreach (var provider in providerList)
                {
                    //fee for particular provider
                    int fee = 0;

                    //query to get list of services for particular provider in the last 7 days
                    var serviceQuery = serviceXML.Descendants("service")
                        .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                                    ((DateTime)x.Element("serviceDate")) <= enddate)
                        .Where(x => x.Element("providerID").Value == provider);

                    //export results into list
                    List<XElement> serviceList = serviceQuery.ToList();

                    //if service list for provider is not empty make a report
                    if (serviceList.Count > 0)
                    {
                        //loop through each service to calculat
                        foreach (var service in serviceList)
                        {
                            fee += Convert.ToInt32(service.Element("serviceFee").Value);
                            totalFee += Convert.ToInt32(service.Element("serviceFee").Value);
                            totalConsultCount++;
                        }
                        //since each node in xml contains all information, we can extract provider name from it
                        writer.Write(serviceList[0].Element("providerName").Value.PadRight(20));
                        writer.Write(serviceList.Count.ToString().PadRight(30));
                        writer.Write(("$" + fee).PadRight(15));
                        writer.WriteLine();
                        provCount++;
                    }
                }
                writer.WriteLine();
                writer.Write("Total number of providers: ".PadRight(35));
                writer.WriteLine(provCount);
                writer.Write("Total number of consultations: ".PadRight(35));
                writer.WriteLine(totalConsultCount);
                writer.Write("Total fee to be paid: ".PadRight(35));
                writer.WriteLine("$" + totalFee);

                Console.WriteLine("Provider summary report is saved to: {0}", path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                writer.Close();
            }

        }

        /// <summary>
        /// Function is to create EFT report of all providers who has given a service in the last 7 days
        /// Function accepts list of provider objects from calling function.
        /// If report creation is successfull, a path for created report file will be printed
        /// </summary>
        /// <param name="providerList"></param>
        public void createEFT()
        {
            //date range
            DateTime startdate = DateTime.Today.Date.AddDays(-7);
            DateTime enddate = DateTime.Today;
            //this is where a report will be saved
            string path = Environment.CurrentDirectory + "/Reports" +"/EFT Report.txt";

            //query to get list of providers who has given a service in the last 7 days
            var provQuery = serviceXML.Descendants("service")
                .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                            ((DateTime)x.Element("serviceDate")) <= enddate);

            //export results into list, get only unique provider ids
            List<string> providerList = provQuery.Select(i => i.Element("providerID").Value).Distinct().ToList();
            StreamWriter writer = new StreamWriter(path);

            try
            {
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
                    //query to get list of services for particular provider in the last 7 days
                    var serviceQuery = serviceXML.Descendants("service")
                        .Where(x => ((DateTime)x.Element("serviceDate")) >= startdate &&
                                    ((DateTime)x.Element("serviceDate")) <= enddate)
                        .Where(x => x.Element("providerID").Value == provider);

                    //export query result into list
                    List<XElement> serviceList = serviceQuery.ToList();

                    //if service list is not empty for particular provider, go through services and calculate fee for provider
                    if (serviceList.Count > 0)
                    {
                        foreach (var service in serviceList)
                        {
                            fee += Convert.ToInt32(service.Element("serviceFee").Value);
                        }
                        //since each node in xml contains all information, we can extract provider id and name from it
                        writer.Write(serviceList[0].Element("providerID").Value.PadRight(20));
                        writer.Write(serviceList[0].Element("providerName").Value.PadRight(20));
                        writer.Write("#bank account number".PadRight(30));
                        writer.Write(("$" + fee).PadRight(15));
                        writer.WriteLine();
                    }
                }
                writer.WriteLine();
                Console.WriteLine("EFT Report is saved to {0}", path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                writer.Close();
            }
        }

        /// <summary>
        /// This is helper function to generate either provider or member reports. It will be called by
        /// getWeeklyProvidersReport or getWeeklyMembersReport
        /// Entity could be either Member or Provider Object, serviceList is list of selected services 
        /// that needs to be printed. Depending on entity type (provider or member) a report will be created. 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="serviceList"></param>
        /// <param name="entityType"></param>
        private void MakeFile(String entityID, String entityName, String entityStrtAddr,
                              String entityCity, String entityState, String entityZip,
                              List<XElement> serviceList, string report_type)
        {
            string path = Environment.CurrentDirectory + "/Reports/";
            path += "/" + report_type + " - " + entityID + ".txt";

            StreamWriter writer = new StreamWriter(path);

            try
            {
                //report header
                writer.WriteLine("********** " + report_type + " *".PadRight(50, '*'));
                writer.WriteLine();
                writer.WriteLine(entityID);
                writer.WriteLine(entityName);
                writer.WriteLine(entityStrtAddr);
                writer.WriteLine(entityCity);
                writer.WriteLine(entityState);
                writer.WriteLine(entityZip);
                writer.WriteLine();

                //generate report file according to type of report
                if (report_type == MEMBER_WEEKLY || report_type == MEMBER_ON_DEMAND)
                {
                    writer.WriteLine();
                    writer.Write("Service Date".PadRight(20));
                    writer.Write("Provider Name".PadRight(15));
                    writer.WriteLine("Service Name".PadRight(15));
                    writer.WriteLine("-".PadRight(50, '-'));
                    foreach (var service in serviceList)
                    {
                        writer.Write(service.Element("serviceDate").Value.PadRight(20));
                        writer.Write(service.Element("providerName").Value.PadRight(15));
                        writer.Write(service.Element("serviceName").Value.PadRight(15));
                        writer.WriteLine();
                    }
                    Console.WriteLine("Report for member {0} is saved to {1}", entityID, path);
                }

                else if (report_type == PROVIDER_WEEKLY || report_type == PROVIDER_ON_DEMAND)
                {
                    int totalFee = 0;
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
                    writer.Write("    " + "Total number of consultations: ");
                    writer.WriteLine(serviceList.Count);
                    writer.Write("    " + "Total service fee: $");
                    writer.WriteLine(totalFee);

                    Console.WriteLine("Report for provider {0} is saved to {1}", entityID, path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                writer.Close();
            }
        }

    }
}
