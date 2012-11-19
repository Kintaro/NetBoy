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
    public sealed class LogicalShiftLeftInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var offset = (int)(((opcode & 0x7C0) >> 6) & 0xFF);
            var rd = opcode & 0x7;
            var rs = (opcode & 0x38) >> 3;

            if ((opcode & 0x4000u) == 0x4000u)
            {
                offset = (short)(executionCore.R(rs).Value & 0xFFu);
                rs = rd;
            }

            executionCore.R(rd).Value = executionCore.R(rs).Value << offset;
            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var offset = (opcode & 0x7C0u) >> 6;
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            if ((opcode & 0x4000u) == 0x4000u)
            {
                return "lsl #" + rd + ", #" + rs + " & 0xFF";
            }

            return "lsl #" + rd + ", #" + rs + ", " + offset;
        }
    }
}
