using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Data.Helper
{
    public static class DTHelper
    {
        public static void DisplayDT(DataTable DT)
        {
            foreach (DataRow row in DT.Rows) // Loop over the rows.
            {
                Console.WriteLine("-------- Row --------"); // Print separator.
                foreach (DataColumn myCol in DT.Columns) // Loop over the rows.
                {
                    Console.Write(myCol + ": "); // Print label.
                    Console.WriteLine(row[myCol]); // Invokes ToString abstract method.
                }
            }
            Console.Read(); // Pause.
            Console.Read(); // Pause.
        }
    }
}
