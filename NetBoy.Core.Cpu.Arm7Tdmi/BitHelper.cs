using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi
{
    /// <summary>
    /// 
    /// </summary>
    public static class BitHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static uint Ror(uint value, uint bits)
        {
            return (uint)((value >> (int)bits) | (value << (int)(32 - bits)));
        }
    }
}
