using NetBoy.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            var engineCore = new EngineCore();
            engineCore.Run("bios.bin", "foo");

            Console.ReadLine();
        }
    }
}
