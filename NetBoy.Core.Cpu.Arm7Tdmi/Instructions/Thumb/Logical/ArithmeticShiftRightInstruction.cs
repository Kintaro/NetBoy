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
    public sealed class ArithmeticShiftRightInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var offset = (opcode & 0x7C0u) >> 6;
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            if ((opcode & 0x4000u) == 0x4000u)
            {
                offset = executionCore.R(rs).Value & 0xFFu;
                rs = rd;
            }

            executionCore.R(rd).Value = executionCore.R(rs).Value >> (int)offset;
            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var offset = (opcode & 0x7C0u) >> 6;
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            if ((opcode & 0x4000u) == 0x4000u)
            {
                return "asr #" + rd + ", #" + rs + " & 0xFF";
            }

            return "asr #" + rd + ", #" + rs + ", " + offset;
        }
    }
}
