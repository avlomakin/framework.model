using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TBD.ConsoleReporter
{
    public class SDataTablePrinter
    {
        public static String GetPrettyDataTable( DataTable table )
        {
            //(avlomakin) yeah i know that realization is not efficient 
            Dictionary<DataColumn, Int32> maxPad = new Dictionary<DataColumn, Int32>();

            foreach (DataColumn tableColumn in table.Columns)
            {
                maxPad[ tableColumn ] = tableColumn.Caption.Length;
                foreach (DataRow tableRow in table.Rows)
                    maxPad[ tableColumn ] = Math.Max( maxPad[ tableColumn ], tableRow[ tableColumn ].ToString().Length );
            }

            using (StringWriter sw = new StringWriter())
            {
                foreach (DataColumn tableColumn in table.Columns)
                    sw.Write( tableColumn.Caption.PadRight( maxPad[tableColumn] ) + " | ");

                sw.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn col in table.Columns)
                        sw.Write(row[col].ToString().PadRight(maxPad[col]) + " | ");
                    sw.WriteLine();
                }

                String output = sw.ToString();
                return output;
            }

        }
    }
}