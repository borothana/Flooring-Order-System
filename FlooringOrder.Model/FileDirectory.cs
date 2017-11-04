using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FlooringOrder.Model
{
    public class FileDirectory
    {
        public static string GetOrderFileName(DateTime date)
        {
            return System.IO.Path.GetFileName(GetOrderFileFullName(date));           
        }
        public static string GetOrderFileFullName(DateTime date)
        {
            string fileName = ConfigurationSettings.AppSettings["OrderPath"];
            //return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            //    + "/Data/" + fileName + "_" + date.ToString("MMddyyyy") + ".txt";
            return fileName + "_" + date.ToString("MMddyyyy") + ".txt";
        }

        public static string GetOrderFileLocation(DateTime date)
        {
            return System.IO.Path.GetDirectoryName(GetOrderFileFullName(date));
        }
    }
}
