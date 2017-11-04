using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringOrder.Model.Response;
using FlooringOrder.BLL;
using FlooringOrder.Model;

namespace FlooringOrder.Test
{
    [TestFixture]
    class OrderTestRepository
    {
        [TestCase("06/01/2015", false)]
        [TestCase("06/01/2014", false)]
        [TestCase("06/01/2013", true)]
        public void CanLookupAccount(DateTime orderDate, bool expected)
        {
            OrderManager orderManager = OrderManagerFactory.create(orderDate);
            DisplayOrderResponse response = orderManager.LookupOrder();

            Assert.AreEqual(expected, response.Success);
        }

        [TestCase("01/02/2017", "Na", "PA", "Tile", 100, true)]
        [TestCase("01/02/2017", "Da", "OH", "Wood", 90, true)]
        [TestCase("05/09/2017", "Pey", "OH", "Tile", 150, true)]
        public void CanAddOrder(DateTime orderDate, string customerName, string state, string productType, decimal area, bool expected )
        {
            OrderManager orderManager = OrderManagerFactory.create(orderDate);

            TaxManager taxManager = TaxManagerFactory.create();
            DisplayTaxResponse taxResponse = taxManager.DisplayTaxResponse(state);
            
            ProductManager productManager = ProductManagerFactory.create();
            DisplayProductResponse productResponse = productManager.DisplayProductResponse(productType);
            
            Orders order = new Orders()
            {
                Area = area,
                CostPerSquareFoot = productResponse.Products.CostPerSquareFoot,
                CustomerName = customerName,
                LaborCostPerSquareFoot = productResponse.Products.LaborCostPerSquareFoot,
                OrderNumber = orderManager.GenerateOrderNumber(),
                ProductType = productResponse.Products.ProductType,
                State = taxResponse.Tax.StateAbbreviation,
                TaxRate = taxResponse.Tax.TaxRate,
            };

            AddOrderResponse orderResponse = orderManager.AddOrder(order);

            Assert.AreEqual(orderResponse.Success, expected);
        }


        [TestCase("01/02/2017", 1, "Daro", "OH", "Wood", 95, true)]
        [TestCase("01/02/2017", 2, "Da", "PA", "Wood", 110, true)]
        [TestCase("05/09/2017", 1, "Pey", "PA", "Tile", 150, true)]
        public void CanEditOrder(DateTime orderDate, int orderNumber, string newCustomerName, string newState, string newProductType, decimal newArea, bool expected)
        {
            OrderManager orderManager = OrderManagerFactory.create(orderDate);

            TaxManager taxManager = TaxManagerFactory.create();
            DisplayTaxResponse taxResponse = taxManager.DisplayTaxResponse(newState);

            ProductManager productManager = ProductManagerFactory.create();
            DisplayProductResponse productResponse = productManager.DisplayProductResponse(newProductType);

            Orders order = new Orders()
            {
                Area = newArea,
                CostPerSquareFoot = productResponse.Products.CostPerSquareFoot,
                CustomerName = newCustomerName,
                LaborCostPerSquareFoot = productResponse.Products.LaborCostPerSquareFoot,
                OrderNumber = orderNumber,
                ProductType = productResponse.Products.ProductType,
                State = taxResponse.Tax.StateAbbreviation,
                TaxRate = taxResponse.Tax.TaxRate,
            };

            EditOrderResponse orderResponse = orderManager.EditOrder(order);

            Assert.AreEqual(orderResponse.Success, expected);
        }

        [TestCase("01/02/2017", 1, "Daro", "OH", "Wood", 95)]
        [TestCase("01/02/2017", 2, "Da", "PA", "Wood", 110)]
        [TestCase("05/09/2017", 1, "Pey", "PA", "Tile", 150)]
        public void CanGetOrder(DateTime orderDate, int orderNumber, string customerName, string state, string productType, decimal area)
        {
            OrderManager orderManager = OrderManagerFactory.create(orderDate);
            Orders order = orderManager.GetOrder(orderNumber);
            Assert.AreEqual(order.CustomerName, customerName);
            Assert.AreEqual(order.State, state);
            Assert.AreEqual(order.ProductType, productType);
            Assert.AreEqual(order.Area, area);
        }

        [TestCase("01/02/2017", 1, true)]
        [TestCase("01/02/2017", 2, true)]
        [TestCase("05/09/2017", 1, true)]
        public void CanRemoveOrder(DateTime orderDate, int orderNumber, bool expected)
        {
            OrderManager orderManager = OrderManagerFactory.create(orderDate);
            Orders order = orderManager.GetOrder(orderNumber);
            
            RemoveOrderResponse removeResponse = orderManager.RemoveOrder(order);

            Assert.AreEqual(removeResponse.Success, expected);
        }
    }
}
