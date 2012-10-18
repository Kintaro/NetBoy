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
    public sealed class MoveNegatedInstruction : ThumbInstruction
    {
        public override void Execute(ExecutionCore executionCore, uint opcode)
        {
            // Move negated source register into destination register
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            executionCore.R[rd].Value = ~executionCore.R[rs].Value;
        }
    }
}
