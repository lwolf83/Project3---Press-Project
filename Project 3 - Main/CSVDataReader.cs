using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public class CSVDataReader
    {
        public static List<string[]> GetDataEntries(String filepath)
        {
            List<string[]> dataEntries = new List<string[]>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    dataEntries.Add(columns);
                }
                dataEntries.RemoveAt(0);
                return dataEntries;
            }
        }
    }
}
