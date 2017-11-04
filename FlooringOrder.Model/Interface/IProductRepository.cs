using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrder.Model.Interface
{
    public interface IProductRepository
    {
        Products DisplayProduct(string productType);
    }
}
