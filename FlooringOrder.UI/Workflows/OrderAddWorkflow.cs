using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.BLL;
using FlooringOrder.Model.Response;
using FlooringOrder.UI;
using FlooringOrder.Model;

namespace FlooringOrder.UI.Workflows
{
    public class OrderAddWorkflow
    {
        public void Execute()
        {
            ConsoleIO.ShowChoice("1.   Add Order");
            DateTime orderDate = ConsoleIO.inputDate("Enter order date (MMddyyyy): ");
            string customerName = ConsoleIO.inputName("Enter customer name: ", false);

            DisplayTaxResponse taxResponse;
            while (true)
            {
                string state = ConsoleIO.inputString("Enter state: ");
                TaxManager taxManager = TaxManagerFactory.create();
                taxResponse = taxManager.DisplayTaxResponse(state);
                if (!taxResponse.Success)
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(taxResponse.Message);
                }
                else break;
            }

            DisplayProductResponse productResponse;
            while (true)
            {
                string productType = ConsoleIO.inputString("Enter product type: ");
                ProductManager productManager = ProductManagerFactory.create();
                productResponse = productManager.DisplayProductResponse(productType);
                if (!productResponse.Success)
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(productResponse.Message);
                }
                else break;
            }
            
            decimal area = ConsoleIO.inputInt("Enter area: ", false);
            OrderManager orderManager = OrderManagerFactory.create(orderDate);
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

            ConsoleIO.ShowSummary();
            ConsoleIO.DisplayOrderDetail(order);
            string result;
            while (true)
            {
                Console.Write("Do you want to place the order?(Y/N): ");
                result = Console.ReadLine();
                if (result.ToUpper() == "Y" || result.ToUpper() == "N") break;
                else Console.Write("Invalid input.");
            }

            if (result.ToUpper() == "N") return;

            
            AddOrderResponse orderResponse = orderManager.AddOrder(order);
            if (orderResponse.Success)
            {
                Console.WriteLine("order saved successfully.");
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(orderResponse.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
