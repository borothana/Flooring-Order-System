using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model;
using FlooringOrder.Model.Interface;
using System.Configuration;
using System.IO;

namespace FlooringOrder.Data
{
    public class ProductRepository : IProductRepository
    {
        string _filePath = ConfigurationSettings.AppSettings["ProductPath"];
        public Products DisplayProduct(string productType)
        {
            return GetProducts(productType, _filePath);
        }

        Products GetProducts(string productType, string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string tmp = "";
                try
                {
                    while ((tmp = sr.ReadLine()) != null)
                    {
                        string[] tmpList = tmp.Split(',');
                        if (tmpList[0].Replace("\"", "").ToUpper() == productType.ToUpper())
                        {
                            Products product = new Products();
                            product.ProductType = productType;
                            product.CostPerSquareFoot = decimal.Parse(tmpList[1].Replace("\"", ""));
                            product.LaborCostPerSquareFoot = decimal.Parse(tmpList[2].Replace("\"", ""));
                            
                            return product;
                        }
                    }
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
    }
}
