using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using System.IO;

namespace UnitTests
{
    [TestFixture]
    class ReportTests
    {
        void FileDiff(string pathToExpectedResult, string pathToActualResult)
        {
            using (StreamReader expectedFile = File.OpenText(pathToExpectedResult))
            {
                using (StreamReader actualFile = File.OpenText(pathToActualResult))
                {
                    string actualContents = actualFile.ReadToEnd();
                    string expectedContents = expectedFile.ReadToEnd();

                    Assert.AreEqual(expectedContents, actualContents);
                }
            }
        }
    
        [TestCase(100000001, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Report - 100000001.txt")]
        [TestCase(100000002, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Report - 100000002.txt")]
        [TestCase(100000003, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Report - 100000003.txt")]
        [TestCase(100000008, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Report - 100000008.txt")]
        [TestCase(100000009, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Report - 100000009.txt")]
        [TestCase(100000010, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Report - 100000010.txt")]
        public void ReportExpected_getMemberReport(int memberID, string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getMemberReport(memberID);
            FileDiff(expectedPath, actualPath);
        }

        [TestCase(100000001, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000001.txt")]
        [TestCase(100000002, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000002.txt")]
        [TestCase(100000003, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000003.txt")]
        [TestCase(100000005, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000005.txt")]
        [TestCase(100000007, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000007.txt")]
        [TestCase(100000009, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000009.txt")]
        public void CheckFileExists_getMemberReport(int memberID, string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getMemberReport(memberID);
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase(1000000011, null)]
        [TestCase(1000000012, null)]
        [TestCase(1000000013, null)]
        [TestCase(1000000100, null)]
        public void ReporNotExpected_getMemberReport(int memberID, string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getMemberReport(memberID);
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase(1000000000, null)]
        [TestCase(1000000001, null)]
        [TestCase(1000000010, null)]
        [TestCase(1000000011, null)]
        public void EmptyDatabase_getMemberReport(int memberID, string expectedPath)
        {
            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            doc.Descendants("service").Remove();
            doc.Save(@Environment.CurrentDirectory + "/XML/CapturedServices.xml");

            doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            var query = doc.Descendants("service");
            List<XElement> content = query.ToList();
            foreach (var item in content)
            {
                Console.WriteLine(item);
            }

            Report report = Report.getInstance;
            string actualPath = report.getMemberReport(memberID);
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase(100000001, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Report - 100000001.txt")]
        [TestCase(100000002, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Report - 100000002.txt")]
        [TestCase(100000004, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Report - 100000004.txt")]
        [TestCase(100000008, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Report - 100000008.txt")]
        [TestCase(100000009, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Report - 100000009.txt")]
        [TestCase(100000010, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Report - 100000010.txt")]
        public void ReportExpected_getProviderReport(int providerID, string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getProviderReport(providerID);
            FileDiff(expectedPath, actualPath);
        }

        [TestCase(100000001, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report - 100000001.txt")]
        [TestCase(100000002, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report - 100000002.txt")]
        [TestCase(100000004, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report - 100000004.txt")]
        [TestCase(100000005, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report - 100000005.txt")]
        [TestCase(100000007, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report - 100000007.txt")]
        [TestCase(100000009, "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report - 100000009.txt")]
        public void CheckFileExists_getProviderReport(int providerID, string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getProviderReport(providerID);
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase(1000000003, null)]
        [TestCase(1000000012, null)]
        [TestCase(1000000013, null)]
        [TestCase(1000000100, null)]
        public void ReporNotExpected_getProviderReport(int providerID, string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getProviderReport(providerID);
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase(1000000000, null)]
        [TestCase(1000000001, null)]
        [TestCase(1000000010, null)]
        [TestCase(1000000011, null)]
        public void EmptyDatabase_getProviderReport(int providerID, string expectedPath)
        {
            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            doc.Descendants("service").Remove();
            doc.Save(@Environment.CurrentDirectory + "/XML/CapturedServices.xml");

            doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            var query = doc.Descendants("service");
            List<XElement> content = query.ToList();
            foreach (var item in content)
            {
                Console.WriteLine(item);
            }

            Report report = Report.getInstance;
            string actualPath = report.getProviderReport(providerID);
            Assert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void ReportExpected_getWeeklyMembersReport()
        {
            //Testing getWeeklyMembersReport by comparing the content of created files with the content of expected files
            //If they are same, test will be successfull, otherwise it will fail

            List<string> expectedFilePaths = new List<string>
            {
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000001.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000003.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000004.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000006.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000007.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000008.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000009.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Member Weekly Report - 100000010.txt"
            };

            Report report = Report.getInstance;
            List<string> actualCreatedPaths = report.getWeeklyMembersReport();
            if (expectedFilePaths.Count == actualCreatedPaths.Count)
            {
                for (int i = 0; i < actualCreatedPaths.Count; i++)
                {
                    FileDiff(expectedFilePaths[i], actualCreatedPaths[i]);
                }
            }
            else
                Assert.Fail();
        }

        [Test]
        public void ReportNotExpected_getWeeklyMembersReport()
        {
            //Testing getWeeklyMembersReport when nobody provided a service in the past 7 days
            //In this case getWeeklyMembersReport should not generate any report
            //Testing is done by first changing the service dates of all captured services to earlier date
            //Then comparing if returned list from getWeeklyMembersReport is actually an empty list

            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            var query = from c in doc.Elements("services").Elements("service")
                        select c;
            foreach (XElement service in query)
            {
                service.Element("serviceDate").Value = "10/01/2014";
            }
            doc.Save(@Environment.CurrentDirectory + "/XML/CapturedServices.xml");

            //empty list
            List<string> expectedFilePaths = new List<string>();
            Report report = Report.getInstance;
            List<string> actualCretedPaths = report.getWeeklyMembersReport();
            Assert.AreEqual(expectedFilePaths, actualCretedPaths);
        }

        [Test]
        public void ReportExpected_getWeeklyProvidersReport()
        {
            //Testing getWeeklyProvidersReport by comparing the content of created files with the content of expected files
            //If they are same, test will be successfull, otherwise it will fail

            List<string> expectedFilePaths = new List<string>
            {
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000001.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000002.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000004.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000005.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000006.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000008.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000007.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000009.txt",
                "C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\ExpectedResults\\Provider Weekly Report - 100000010.txt"
            };

            Report report = Report.getInstance;
            List<string> actualCreatedPaths = report.getWeeklyProvidersReport();
            if (expectedFilePaths.Count == actualCreatedPaths.Count)
            {
                for (int i = 0; i < actualCreatedPaths.Count; i++)
                {
                    FileDiff(expectedFilePaths[i], actualCreatedPaths[i]);
                }
            }
            else
                Assert.Fail();
        }

        [Test]
        public void ReportNotExpected_getWeeklyProvidersReport()
        {
            //Testing getWeeklyProvidersReport when nobody provided a service in the past 7 days
            //In this case getWeeklyProvidersReport should not generate any report
            //Testing is done by first changing the service dates of all captured services to earlier date
            //Then comparing if returned list from getWeeklyProvidersReport is actually an empty list

            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            var query = from c in doc.Elements("services").Elements("service")
                        select c;
            foreach (XElement service in query)
            {
                service.Element("serviceDate").Value = "10/01/2014";
            }
            doc.Save(@Environment.CurrentDirectory + "/XML/CapturedServices.xml");

            //empty list
            List<string> expectedFilePaths = new List<string>();
            Report report = Report.getInstance;
            List<string> actualCretedPaths = report.getWeeklyProvidersReport();
            Assert.AreEqual(expectedFilePaths, actualCretedPaths);
        }

        [TestCase("C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report Summary.txt")]
        public void ReportExpected_getProviderSummaryReport(string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getProviderSummary();
            FileDiff(expectedPath, actualPath);
        }
        [TestCase("C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Provider Report Summary.txt")]
        public void CheckPath_getProviderSummaryReport(string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.getProviderSummary();
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase("")]
        public void ReportNotExpected_getProviderSummaryReport(string expectedPath)
        {
            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            var query = from c in doc.Elements("services").Elements("service")
                        select c;
            foreach (XElement service in query)
            {
                service.Element("serviceDate").Value = "10/01/2014";
            }

            doc.Save(@Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            Report report = Report.getInstance;
            string actualPath = report.getProviderSummary();
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase("C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\EFT Report.txt")]
        public void ReportExpected_createEFT(string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.createEFT();
            FileDiff(expectedPath, actualPath);
        }
        [TestCase("C:\\working\\431_Term_Project\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\EFT Report.txt")]
        public void CheckPath_createEFT(string expectedPath)
        {
            Report report = Report.getInstance;
            string actualPath = report.createEFT();
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase("")]
        public void ReportNotExpected_createEFT(string expectedPath)
        {
            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            var query = from c in doc.Elements("services").Elements("service")
                        select c;
            foreach (XElement service in query)
            {
                service.Element("serviceDate").Value = "10/01/2014";
            }

            doc.Save(@Environment.CurrentDirectory + "/XML/CapturedServices.xml");
            Report report = Report.getInstance;
            string actualPath = report.createEFT();
            Assert.AreEqual(expectedPath, actualPath);
        }
    }

}

