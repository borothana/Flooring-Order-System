using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrder.Model.Response
{
    public class EditOrderResponse:Response
    {
        public string CustomerName { get; set; }
        public string State { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public Orders orders { get; set; }
    }
}
