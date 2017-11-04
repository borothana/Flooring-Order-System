using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.UI.Workflows;

namespace FlooringOrder.UI
{
    public class Menu
    {
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***                    ***");
                Console.WriteLine("*    Flooring Program    *");
                Console.WriteLine("***                    ***");
                Console.WriteLine("");
                Console.WriteLine("1.   Display Orders");
                Console.WriteLine("2.   Add Orders");
                Console.WriteLine("3.   Edit Orders");
                Console.WriteLine("4.   Remove Orders");

                Console.WriteLine("\nQ to quit. ");
                Console.Write("Enter Selection: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        OrderLookupWorkflow orderLookup = new OrderLookupWorkflow();
                        orderLookup.Execute();
                        break;
                    case "2":
                        OrderAddWorkflow orderAdd = new OrderAddWorkflow();
                        orderAdd.Execute();
                        break;
                    case "3":
                        OrderEditWorkflow orderEdit = new OrderEditWorkflow();
                        orderEdit.Execute();
                        break;
                    case "4":
                        OrderRemoveWorkflow orderRemove = new OrderRemoveWorkflow();
                        orderRemove.Execute();
                        break;
                    case "q":
                    case "Q":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}
