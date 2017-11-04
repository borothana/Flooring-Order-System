using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlooringOrder.Model.Response
{
    public class RemoveOrderResponse : Response
    {
        public DateTime OrderDate { get; set; }
        public string OrderNumer { get; set; }
    }
}
