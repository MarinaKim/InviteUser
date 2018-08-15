using InviteUser.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortHuman
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1200);
                    WorkUser.SortHuman();
                    Console.WriteLine("*");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            
        }
    }
}
