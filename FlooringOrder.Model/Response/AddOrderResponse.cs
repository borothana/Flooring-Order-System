using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrder.Model.Response
{
    public class AddOrderResponse:Response
    {
        public Orders orders { get; set; }
    }
}
