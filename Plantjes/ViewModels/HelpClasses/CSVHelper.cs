using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels.HelpClasses
{
    internal class CSVHelper
    {
        public static List<String[]> CSVToMemberList(string csvLocation)
        {
            List<String[]> valueList = new List<String[]>();
            using (StreamReader reader = new StreamReader(csvLocation))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] readValues = line.Split(';');
                    if(readValues[0] == "e-mail"){}
                    else
                    {
                        valueList.Add(readValues);
                    }
                }
            }
            return valueList;
        }
    }
}
