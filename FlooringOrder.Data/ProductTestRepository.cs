using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model;
using FlooringOrder.Model.Interface;

namespace FlooringOrder.Data
{
    public class ProductTestRepository : IProductRepository
    {
        public Products DisplayProduct(string productType)
        {
            if(productType.ToUpper() == "HEARTWOOD")
            {
                return new Products() { ProductType = "HeartWood", CostPerSquareFoot = 25.5m, LaborCostPerSquareFoot = 15.75m };
            }
            else
            {
                return null;
            }
            
        }
    }
}
