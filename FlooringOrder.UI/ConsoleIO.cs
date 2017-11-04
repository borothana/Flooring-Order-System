using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model;
using System.IO;
using System.Configuration;
using FlooringOrder.BLL;
using FlooringOrder.Model.Response;

namespace FlooringOrder.UI
{
    public class ConsoleIO
    {
        static string OrderFilePath = ConfigurationSettings.AppSettings["OrderPath"];

        public static void ShowChoice(string title)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine("---------------------");
        }

        public static void ShowSummary()
        {
            Console.WriteLine("");
            Console.WriteLine("Order Summary:");
            Console.WriteLine("----------------");
        }

        public static void DisplayOrderDetail(Orders orders)
        {
            Console.WriteLine("");
            Console.WriteLine($"Order Number: {orders.OrderNumber}");
            Console.WriteLine($"Customer Name: {orders.CustomerName}");
            Console.WriteLine($"State: {orders.State}");
            Console.WriteLine($"Product: {orders.ProductType}");
            Console.WriteLine($"Area: {orders.Area}");
            Console.WriteLine($"Materials: {orders.MaterialCost}");
            Console.WriteLine($"Labor: {orders.LaborCost}");
            Console.WriteLine($"Tax: {orders.Tax}");
            Console.WriteLine($"Total: {orders.Total}");
        }

        public static string inputString(string message)
        {
            string result;
            while (true)
            {
                Console.Write(message);
                result = Console.ReadLine(); result = result.ToUpper();
                if (result != "") return result;
                
                Console.WriteLine("Invalid input!");
                continue;                
            }
        }

        public static DateTime inputDate(string message)
        {
            DateTime result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (input.Length != 8 || !DateTime.TryParse(input.Substring(0, 2) + "/" + input.Substring(2, 2) + "/" + input.Substring(4), out result))
                    Console.WriteLine("Invalid input!");
                else
                    return result;
            }
        }

        public static int inputInt(string message, bool allowBlank)
        {
            int result; 
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (allowBlank == true && input.Trim() == "") return -1;
                if (int.TryParse(input, out result))
                    return result;
                else
                    Console.WriteLine("Invalid input!");
            }
        }
        
        public static string inputName(string message, bool allowBlank)
        {
            string result, tmpResult;
            while (true)
            {
                bool meetCriteria = true;
                Console.Write(message);
                result = Console.ReadLine(); result = result.Trim();
                if (result == "" && allowBlank == true) return result;
                else if(result == "")
                {
                    Console.Write("Invalid input!");
                    continue;
                }

                tmpResult = result.ToUpper();
                foreach (char c in tmpResult)
                {
                    if (!(c == '.' || c == ',' || ((int)c >= 65 && (int)c <= 99)))
                    {
                        meetCriteria = false;
                        Console.WriteLine("Invalid input!");
                        break;
                    }
                }
                if (meetCriteria == true) break;
            }
            return result;
        }
        
    }
}

