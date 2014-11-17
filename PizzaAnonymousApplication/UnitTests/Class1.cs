using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PizzaAnonymousApplication;

namespace UnitTests
{
    [TestFixture]
    class Class1
    {
     
        [Test]
        public void test1()
        {
            PizzaAnonymous pizza1 = PizzaAnonymous.instance();
            PizzaAnonymous pizza2 = PizzaAnonymous.instance();

            Assert.AreSame(pizza1, pizza2);
        }

        public void test2()
        {
            Service service1;
        }
    }
}
