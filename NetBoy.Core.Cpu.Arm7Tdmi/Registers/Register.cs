﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Registers
{
    /// <summary>
    /// 
    /// </summary>
    public class Register
    {
        public uint Value;

        public override string ToString()
        {
            return string.Format("{0:X}", this.Value);
        }
    }
}
