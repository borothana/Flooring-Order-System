using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model.Response;

namespace FlooringOrder.Model.Interface
{
    public interface ITaxRepository
    {
        Taxs DisplayTax(string state);
    }
}
