using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrder.Model.Interface
{
    public interface IOrderRepository
    {        
        List<Orders> DisplayOrder();
        Orders GetOrder(int orderNumber);
        bool AddOrder(Orders order);
        bool EditOrder(Orders order);
        bool RemoveOrder(Orders order);
        int GetOrderNumber();
        bool IfFileExist();
    }
}
