using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductList.Controllers;

namespace ProductList
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDetails()
        {
            var controller = new ProductList.Controllers.ProductsController();
        }
    }
}
