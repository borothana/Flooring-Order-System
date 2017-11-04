using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.BLL;
using FlooringOrder.Model.Response;
using FlooringOrder.UI;
using FlooringOrder.Model;
using System.IO;
using System.Configuration;

namespace FlooringOrder.UI.Workflows
{
    public class OrderEditWorkflow
    {
        public void Execute()
        {
            ConsoleIO.ShowChoice("2.   Edit Order");

            DateTime orderDate;
            OrderManager orderManager;

            while (true)
            {
                orderDate = ConsoleIO.inputDate("Enter order date (MMddyyyy): ");
                orderManager = OrderManagerFactory.create(orderDate);
                if (!orderManager.IfFileExist())
                {
                    Console.WriteLine("No order in this date!");
                    continue;
                }
                else
                {
                    break;
                }                    
            }


            Orders order;
            while (true)
            {
                int orderNumber = ConsoleIO.inputInt("Enter order number: ", false);
                order = orderManager.GetOrder(orderNumber);
                if (order is null) continue;
                break;
            }
            
            string customerName = ConsoleIO.inputName($"Enter customer name({order.CustomerName}): ", true);
            if ((customerName + "").Trim() == "") customerName = order.CustomerName;

            DisplayTaxResponse taxResponse;
            while (true)
            {
                Console.Write($"Enter state({order.State}): ");
                string state = Console.ReadLine();

                if ((state + "").Trim() == "")
                    state = order.State;

                TaxManager taxManager = TaxManagerFactory.create();
                taxResponse = taxManager.DisplayTaxResponse(state.ToUpper());
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
                Console.Write($"Enter product type({order.ProductType}): ");
                string productType = Console.ReadLine();
                if ((productType + "").Trim() == "") productType = order.ProductType;

                ProductManager productManager = ProductManagerFactory.create();
                productResponse = productManager.DisplayProductResponse(productType);
                if (!productResponse.Success)
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(productResponse.Message);
                }
                else break;
            }

            decimal area = ConsoleIO.inputInt($"Enter product type({order.Area}): ", true);
            if (area == -1) area = order.Area;

            order = new Orders()
            {
                Area = area,
                CostPerSquareFoot = productResponse.Products.CostPerSquareFoot,
                CustomerName = customerName,
                LaborCostPerSquareFoot = productResponse.Products.LaborCostPerSquareFoot,
                OrderNumber = order.OrderNumber,
                ProductType = productResponse.Products.ProductType,
                State = taxResponse.Tax.StateAbbreviation,
                TaxRate = taxResponse.Tax.TaxRate,
            };

            ConsoleIO.ShowSummary();
            ConsoleIO.DisplayOrderDetail(order);
            string result;
            while (true)
            {
                Console.Write("Do you want to save the order?(Y/N): ");
                result = Console.ReadLine();
                if (result.ToUpper() == "Y" || result.ToUpper() == "N") break;
                else Console.Write("Invalid input.");
            }

            if (result.ToUpper() == "N") return;

            EditOrderResponse orderResponse = orderManager.EditOrder(order);
            if (orderResponse.Success)
            {
                Console.WriteLine("Order edited successfully.");
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
