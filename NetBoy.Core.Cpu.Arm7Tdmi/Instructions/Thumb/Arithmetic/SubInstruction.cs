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
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            if ((opcode & 0xF800u) >> 11 == 3)
            {
                short rd = (short)(opcode & 0x7u);
                short rs = (short)((opcode & 0x38u) >> 3);
                short rn = (short)((opcode & 0x1C0u) >> 6);

                var op = (opcode & 0x600u) >> 9;
                short rsV = (short)(executionCore.R(rs).Value & 0xFFu);
                short rnV = (short)(executionCore.R(rn).Value & 0xFFu);

                if (op == 1)
                    executionCore.R(rd).Value = (uint)(rsV - rnV);
                else if (op == 3)
                    executionCore.R(rd).Value = (uint)(rsV - rn);
            }
            else if ((opcode & 0xF800u) >> 11 == 7)
            {
                short rd = (short)((opcode & 0x700u) >> 8);
                short nn = (short)(opcode & 0xFFu);
                short rdV = (short)(executionCore.R(rd).Value & 0xFFu);

                executionCore.R(rd).Value = (uint)(rdV - nn);
            }
            return false;
        }

        public override string InstructionAsString(ushort opcode)
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
