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
                    executionCore.R(rd).Value = (uint)((short)rsV + (short)rn);

                if (op == 0)
                {
                    executionCore.CurrentProgramStatusRegister.Signed = (((ushort)(int)(rsV + (int)rnV)) & 0x8000u) != 0;
                    executionCore.CurrentProgramStatusRegister.Zero = ((ushort)((int)rsV + (int)rnV)) == 0;
                    executionCore.CurrentProgramStatusRegister.Overflow = ((uint)((int)rsV + (int)rnV)) > short.MaxValue;
                }
                else if (op == 2)
                {
                    short a = (short)rsV;
                    short b = (short)rn;
                    var result = (uint)a + b;
                    executionCore.CurrentProgramStatusRegister.Signed = (((ushort)((int)rsV + (int)rn)) & 0x8000u) != 0;
                    executionCore.CurrentProgramStatusRegister.Zero = ((ushort)((int)rsV + (int)rn)) == 0;      
                    executionCore.CurrentProgramStatusRegister.Overflow = ((uint)((short)rsV + (short)rn)) > uint.MaxValue;
                }       
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
