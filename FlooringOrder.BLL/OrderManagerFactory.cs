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
    public static class OrderManagerFactory
    {
        public static OrderManager create(DateTime date)
        {
            string mode = ConfigurationSettings.AppSettings["Mode"];
            switch (mode)
            {
                case "File":
                    return new OrderManager(new FileOrderRepository(date));
                case "Test":
                    return new OrderManager(new OrderTestRepository(date));
            }
            throw new Exception("Invalid mode.");
        }
    }
}
