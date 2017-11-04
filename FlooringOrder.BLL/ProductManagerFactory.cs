using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model.Interface;
using FlooringOrder.Data;
using System.Configuration;

namespace FlooringOrder.BLL
{
    public static class ProductManagerFactory
    {
        public static ProductManager create()
        {
            
            string mode = ConfigurationSettings.AppSettings["Mode"];
            switch (mode)
            {
                case "File":
                    return new ProductManager(new ProductRepository());
                case "Test":
                    return new ProductManager(new ProductTestRepository());
            }
            throw new Exception("Invalid mode.");
        }
    }
}
