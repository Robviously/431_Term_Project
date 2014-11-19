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
        void Diff(string pathToExpectedResult, string pathToActualResult)
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
            Diff(expectedPath, actualPath);
        }

        [TestCase(100000001, "C:\\Users\\Ruhshod\\Desktop\\GitHub\\431_Term_Project\\Reports\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000001.txt")]
        [TestCase(100000002, "C:\\Users\\Ruhshod\\Desktop\\GitHub\\431_Term_Project\\Reports\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000002.txt")]
        [TestCase(100000003, "C:\\Users\\Ruhshod\\Desktop\\GitHub\\431_Term_Project\\Reports\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000003.txt")]
        [TestCase(100000008, "C:\\Users\\Ruhshod\\Desktop\\GitHub\\431_Term_Project\\Reports\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000008.txt")]
        [TestCase(100000009, "C:\\Users\\Ruhshod\\Desktop\\GitHub\\431_Term_Project\\Reports\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000009.txt")]
        [TestCase(100000010, "C:\\Users\\Ruhshod\\Desktop\\GitHub\\431_Term_Project\\Reports\\PizzaAnonymousApplication\\UnitTests\\bin\\Debug\\Reports\\Member Report - 100000010.txt")]
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
    }
}

