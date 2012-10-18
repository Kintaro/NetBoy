using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Logical
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NopInstruction : ThumbInstruction
    {
        public override void Execute(ExecutionCore executionCore, uint opcode)
        {
        }
    }
}
