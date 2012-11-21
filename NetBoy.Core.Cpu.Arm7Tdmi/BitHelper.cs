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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static bool CarryRor(uint value, uint bits)
        {
            return ((value >> (int)(bits - 1)) & 0x1) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static uint Asr(uint value, uint bits)
        {
            var t = (int)value;
            return (uint)(t >> (int)bits);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static bool CarryAsr(uint value, uint bits)
        {
            return ((value >> (int)(bits - 1)) & 0x1) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static uint Lsl(uint value, uint bits)
        {
            return (uint)(value << (int)bits);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static bool CarryLsl(uint value, uint bits)
        {
            return ((value << (int)(bits - 1)) & 0x80000000u) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static uint Lsr(uint value, uint bits)
        {
            return (uint)(value >> (int)bits);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static bool CarryLsr(uint value, uint bits)
        {
            return ((value >> (int)(bits - 1)) & 0x1) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static uint ShiftByType(uint value, uint bits, uint type)
        {
            switch (type)
            {
                case 0: return Lsl(value, bits);
                case 1: return Lsr(value, bits);
                case 2: return Asr(value, bits);
                case 3: return Ror(value, bits);
                default: throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bits"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool CarryByType(uint value, uint bits, uint type)
        {
            switch (type)
            {
                case 0: return CarryLsl(value, bits);
                case 1: return CarryLsr(value, bits);
                case 2: return CarryAsr(value, bits);
                case 3: return CarryRor(value, bits);
                default: throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ShiftTypeAsString(uint type)
        {
            switch (type)
            {
                case 0: return "lsl";
                case 1: return "lsr";
                case 2: return "asr";
                case 3: return "ror";
                default: throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int BitCount(uint n)
        {
            var ret = 0;
            while (n != 0)
            {
                n &= (n - 1);
                ret++;
            }
            return ret;
        }
    }
}
