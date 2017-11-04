using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model.Response;
using FlooringOrder.BLL;
using FlooringOrder.UI;
using System.IO;
using System.Configuration;
using FlooringOrder.Model;

namespace FlooringOrder.UI.Workflows
{
    public class OrderLookupWorkflow
    {
        public void Execute()
        {
            ConsoleIO.ShowChoice("1.   Order Lookup");
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

            DisplayOrderResponse response = orderManager.LookupOrder();
            if (response.Success)
            {
                for(int i = 0; i<response.orders.Count; i++)
                    ConsoleIO.DisplayOrderDetail(response.orders[i]);
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
