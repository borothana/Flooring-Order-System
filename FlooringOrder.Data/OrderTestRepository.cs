using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model;
using FlooringOrder.Model.Interface;


namespace FlooringOrder.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        private static List<Orders> _orderList = new List<Orders>() {
                                                                    new Orders()
                                                                                {
                                                                                    Area = 100,
                                                                                    CostPerSquareFoot = 25.5m,
                                                                                    CustomerName = "Borothana",
                                                                                    LaborCostPerSquareFoot = 15.75m,
                                                                                    OrderNumber = 1,
                                                                                    ProductType = "HeartWood",
                                                                                    State = "MN",
                                                                                    TaxRate = 6.0m,
                                                                                }
                                                                    };


        DateTime _date;
        public OrderTestRepository(DateTime date)
        {
            _date = date;
        }

        public bool AddOrder(Orders order)
        {   
            _orderList.Add(order);
            return true;
        }

        public List<Orders> DisplayOrder()
        {
            return _orderList;
        }

        public bool EditOrder(Orders order)
        {
            List<Orders> tmp = _orderList.Where(c => c.OrderNumber == order.OrderNumber).ToList();
            _orderList[_orderList.IndexOf(tmp[0])] = order;
            return true;
        }
        
        public bool RemoveOrder(Orders order)
        {
            _orderList.RemoveAt(_orderList.IndexOf(order));
            return true;
        }
        
        public int GetOrderNumber()
        {
            if (_orderList.Count > 0)
            {
                return _orderList.OrderByDescending(c => c.OrderNumber).Select(c => c.OrderNumber).Max() + 1;
            }
            else
            {
                return 1;
            }
        }

        public Orders GetOrder(int orderNumber)
        {

            if (_orderList.Where(c => c.OrderNumber == orderNumber).Count() > 0)
            {
                return _orderList.Where(c => c.OrderNumber == orderNumber).ToList()[0];
            }
            return null;
        }

        public bool IfFileExist()
        {
            return _date.Date == DateTime.Now.Date;
        }
    }
}
