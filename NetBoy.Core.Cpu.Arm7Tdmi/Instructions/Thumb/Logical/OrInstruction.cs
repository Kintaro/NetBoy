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
    public sealed class OrInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            executionCore.R(rd).Value = executionCore.R(rd).Value | executionCore.R(rs).Value;
            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            return "orr #" + rd + ", #" + rs;
        }
    }
}
