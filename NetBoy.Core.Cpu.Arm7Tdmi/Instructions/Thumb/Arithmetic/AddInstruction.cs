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
    public sealed class AddInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            if ((opcode & 0xF800u) >> 11 == 3)
            {
                var rd = opcode & 0x7u;
                var rs = (opcode & 0x38u) >> 3;
                var rn = (opcode & 0x1C0u) >> 6;

                var op = (opcode & 0x600u) >> 9;

                short rsV = (short)executionCore.R(rs).Value;
                short rnV = (short)executionCore.R(rn).Value;
                short r = 0;

                try
                {
                    r = checked((short)(rsV + rnV));
                }
                catch (OverflowException)
                {
                    executionCore.CurrentProgramStatusRegister.Overflow = true;
                }

                if (op == 0)
                    executionCore.R(rd).Value = (uint)r;
                else if (op == 2)
                    executionCore.R(rd).Value = (uint)(rsV + (short)rn);
            }
            else if ((opcode & 0xF800u) >> 11 == 6)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = opcode & 0xFFu;

                var rdV = (short)executionCore.R(rd).Value;
                var rnV = (short)nn;
                short r = 0;

                try
                {
                    r = checked((short)(rdV + rnV));
                }
                catch (OverflowException)
                {
                    executionCore.CurrentProgramStatusRegister.Overflow = true;
                }

                executionCore.R(rd).Value = (uint)r;
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
                    return "add #" + rd + ", #" + rs + ", #" + rn;
                else if (op == 2)
                    return "add #" + rd + ", #" + rs + ", " + rn;
            }
            else if ((opcode & 0xF800u) >> 11 == 6)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = opcode & 0xFFu;

                return "add #" + rd + ", " + nn;
            }

            throw new NotSupportedException();
        } 
    }
}
