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
    public class OrderRemoveWorkflow
    {
        public void Execute()
        {
            ConsoleIO.ShowChoice("3.   Remove Order");
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

            ConsoleIO.DisplayOrderDetail(order);

            Console.Write("Are you sure want to remove?(Y/N): ");
            string result = Console.ReadLine();
            if (result.ToUpper() == "Y")
            {
                RemoveOrderResponse removeResponse = orderManager.RemoveOrder(order);
                if (removeResponse.Success)
                {
                    Console.WriteLine("Order removed successful.");                    
                }
                else
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(removeResponse.Message);
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
