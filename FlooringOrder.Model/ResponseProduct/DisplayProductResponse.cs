using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model;

namespace FlooringOrder.Model.Response
{
    public class DisplayProductResponse:Response
    {
        public Products Products { get; set; }
    }
}
