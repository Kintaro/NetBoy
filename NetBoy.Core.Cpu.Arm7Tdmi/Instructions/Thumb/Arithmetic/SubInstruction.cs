using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Arithmetic
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SubInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            if ((opcode & 0xF800u) >> 11 == 3)
            {
                var rd = opcode & 0x7u;
                var rs = (opcode & 0x38u) >> 3;
                var rn = (opcode & 0x1C0u) >> 6;

                var op = (opcode & 0x600u) >> 9;

                if (op == 1)
                    executionCore.R(rd).Value = executionCore.R(rs).Value - executionCore.R(rn).Value;
                else if (op == 3)
                    executionCore.R(rd).Value = executionCore.R(rs).Value - rn;
            }
            else if ((opcode & 0xF800u) >> 11 == 7)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = opcode & 0xFFu;

                executionCore.R(rd).Value = executionCore.R(rd).Value - nn;
            }
            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            if ((opcode & 0xF800u) >> 11 == 3)
            {
                var rd = opcode & 0x7u;
                var rs = (opcode & 0x38u) >> 3;
                var rn = (opcode & 0x1C0u) >> 6;

                var op = (opcode & 0x600u) >> 9;

                if (op == 0)
                    return "sub #" + rd + ", #" + rs + ", #" + rn;
                else if (op == 2)
                    return "sub #" + rd + ", #" + rs + ", " + rn;
            }
            else if ((opcode & 0xF800u) >> 11 == 6)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = opcode & 0xFFu;

                return "sub #" + rd + ", " + nn;
            }

            throw new NotSupportedException();
        }
    }
}
