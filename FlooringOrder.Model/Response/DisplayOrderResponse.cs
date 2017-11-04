using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrder.Model.Response
{
    public class DisplayOrderResponse:Response
    {
        public List<Orders> orders { get; set; }
    }
}
