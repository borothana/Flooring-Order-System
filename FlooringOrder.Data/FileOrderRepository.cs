using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model.Interface;
using FlooringOrder.Model;
using System.Configuration;
using System.IO;

namespace FlooringOrder.Data
{
    public class FileOrderRepository : IOrderRepository
    {
        string _filePath;
        public FileOrderRepository(DateTime date)
        {
            _filePath = FileDirectory.GetOrderFileFullName(date);            
        }
        public List<Orders> DisplayOrder()
        {
            return GetOrder(_filePath);
        }

        public List<Orders> GetOrder(string path)
        {
            if (!File.Exists(path)) return null;

            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string tmp = "";
                try
                {
                    List<Orders> result = new List<Orders>();
                    while ((tmp = sr.ReadLine()) != null)
                    {
                        string[] tmpList = tmp.Split(',');
                        Orders order = new Orders();
                        order.OrderNumber = int.Parse(tmpList[0].Replace("\"", ""));
                        order.CustomerName = tmpList[1].Replace("\"", "");
                        order.State = tmpList[2].Replace("\"", "");
                        order.TaxRate = decimal.Parse(tmpList[3].Replace("\"", ""));
                        order.ProductType = tmpList[4].Replace("\"", "");
                        order.Area = decimal.Parse(tmpList[5].Replace("\"", ""));
                        order.CostPerSquareFoot = decimal.Parse(tmpList[6].Replace("\"", ""));
                        order.LaborCostPerSquareFoot = decimal.Parse(tmpList[7].Replace("\"", ""));
                        //order.MaterialCost = decimal.Parse(tmpList[8].Replace("\"", ""));
                        //order.LaborCost = decimal.Parse(tmpList[9].Replace("\"", ""));
                        //order.Tax = decimal.Parse(tmpList[10].Replace("\"", ""));
                        //order.Total = decimal.Parse(tmpList[11].Replace("\"", ""));

                        result.Add(order);
                    }
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return null;
        }
        
        public bool AddOrder(Orders order)
        {
            string str = $"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType}" +
                           $",{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost}" +
                           $",{order.LaborCost},{order.Tax},{order.Total}";
            return SaveToFile(str, _filePath);
        }
        
        public bool EditOrder(Orders order)
        {
            List<string> list = MoveData(order, _filePath, false);
            if (list != null) return SaveToFile(list, _filePath);
            return false;
        }

        public bool RemoveOrder(Orders order)
        {
            List<string> list = MoveData(order, _filePath, true);
            if (list != null)
            {
                if(SaveToFile(list, _filePath))
                {
                    //check if there is no order, delete file
                    List<Orders> tmp = GetOrder(_filePath);
                    if (tmp.Count<=0) File.Delete(_filePath);
                    return true;
                }
            }
            return false;
        }

        public List<string> MoveData(Orders order, string path, bool skipOrder)
        {
            try
            {
                if (!File.Exists(path)) return null;

                string tmpPath = Path.GetDirectoryName(path) + "\\tmp_" + Path.GetFileName(path);
                File.Move(path, tmpPath);
                List<string> orderList = ListOrder(order, tmpPath, skipOrder);
                File.Delete(tmpPath);

                return orderList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return null;
            }
        }
        
        public bool SaveToFile(string order, string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                        sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                }

                using (StreamWriter sw = File.AppendText(path))
                    sw.WriteLine(order);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }
            return true;
        }


        public bool SaveToFile(List<string> orderList, string path)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                    for (int i = 0; i < orderList.Count; i++)
                        sw.WriteLine(orderList[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        public List<string> ListOrder(Orders order, string path, bool skipOrder)
        {
            List<string> result = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    sr.ReadLine();
                    string tmp = "";
                    while ((tmp = sr.ReadLine()) != null)
                    {
                        string[] tmpList = tmp.Split(',');
                        if (int.Parse(tmpList[0].Replace("\"", "")) == order.OrderNumber)
                        {
                            if (skipOrder == false)
                            {
                                result.Add($"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate}" +
                                    $",{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost}" +
                                    $",{order.Tax},{order.Total}");
                            }
                        }
                        else
                            result.Add(tmp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return null;
            }
            return result;
        }

        public int GetOrderNumber()
        {
            List<Orders> orderList = GetOrder(_filePath);
            if (orderList is null) return 1;
            return orderList.Select(c => c.OrderNumber).Max() + 1;
        }

        public Orders GetOrder(int orderNumber)
        {
            List<Orders> orderList = GetOrder(_filePath);
            if (orderList is null) return null;
            if (orderList.Where(c => c.OrderNumber == orderNumber).Count() > 0)
            {
                return orderList.Where(c => c.OrderNumber == orderNumber).ToList()[0];
            }
            return null;
        }

        public bool IfFileExist()
        {
            return File.Exists(_filePath);
        }
    }
}
