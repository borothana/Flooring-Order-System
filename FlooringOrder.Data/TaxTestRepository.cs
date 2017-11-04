using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model;
using FlooringOrder.Model.Interface;

namespace FlooringOrder.Data
{
    public class TaxTestRepository : ITaxRepository
    {
        public Taxs DisplayTax(string state)
        {
            if (state.ToUpper() == "MN")
            {
                return new Taxs() { StateAbbreviation = "MN", StateName = "Minnesota", TaxRate = 6.0m };
            }
            else
            {
                return null;
            }
            
        }
    }
}
