using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model.Interface;
using FlooringOrder.Model.Response;
using FlooringOrder.Model;
using System.Configuration;
using System.IO;

namespace FlooringOrder.Data
{
    public class TaxRepository : ITaxRepository
    {
        string _filePath = ConfigurationSettings.AppSettings["TaxPath"];
        public Taxs DisplayTax(string state)
        {
            return GetTax(state, _filePath);
        }

        public Taxs GetTax(string state, string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string tmp = "";
                try
                {

                    while ((tmp = sr.ReadLine()) != null)
                    {
                        string[] tmpList = tmp.Split(',');
                        if (tmpList[0].Replace("\"", "").ToUpper() == state.ToUpper())
                        {
                            Taxs tax = new Taxs();
                            tax.StateAbbreviation = state;
                            tax.StateName = tmpList[1].Replace("\"", ""); 
                            tax.TaxRate = decimal.Parse(tmpList[2].Replace("\"", ""));
                            
                            return tax;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return null;
        }

    }
}
