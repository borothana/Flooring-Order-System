using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Data;
using System.Configuration;

namespace FlooringOrder.BLL
{
    public static class TaxManagerFactory
    {
        public static TaxManager create()
        {
            string mode = ConfigurationSettings.AppSettings["Mode"];
            switch(mode){
                case "File":
                    return new TaxManager(new TaxRepository());
                case "Test":
                    return new TaxManager(new TaxTestRepository());
            }
            throw new Exception("Invalid mode.");            
        }
    }
}
