﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ThumbInstruction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionCore"></param>
        /// <param name="opcode"></param>
        public abstract bool Execute(ExecutionCore executionCore, uint opcode);
        /// <summary>
        /// 
        /// </summary>
        public virtual string InstructionAsString(uint opcode)
        {
            return "not yet represented";
        }
    }
}
