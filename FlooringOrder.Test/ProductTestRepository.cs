using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringOrder.Model;
using FlooringOrder.Data;
using FlooringOrder.BLL;
using FlooringOrder.Model.Response;

namespace FlooringOrder.Test
{
    [TestFixture]
    class ProductTestRepository
    {
        [TestCase("Carpet", true)]
        [TestCase("Laminate", true)]
        [TestCase("Tile", true)]
        [TestCase("Heartwood", false)]
        [TestCase("Wood", true)]
        public void CanGetTax(string productType, bool expected)
        {
            ProductManager productManager = ProductManagerFactory.create();
            DisplayProductResponse productResponse = productManager.DisplayProductResponse(productType);

            Assert.AreEqual(productResponse.Success, expected);
        }
    }
}
