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
    class TaxTestRepository
    {
        [TestCase("OH", "Ohio", 6.25, true)]
        [TestCase("MN", "Minnesota", 6.00, false)]
        [TestCase("PA", "Pennsylvania", 6.75, true)]
        public void CanGetTax(string stateAbreviation, string state, decimal taxRate, bool expected)
        {
            TaxManager taxManager = TaxManagerFactory.create();
            DisplayTaxResponse taxResponse = taxManager.DisplayTaxResponse(stateAbreviation);
            
            Assert.AreEqual(taxResponse.Success, expected);
        }

    }
}
