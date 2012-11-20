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

                var rsV = executionCore.R(rs).Value;
                var rnV = executionCore.R(rn).Value;
                var r = 0;

                if (op == 0)
                    executionCore.R(rd).Value = (uint)r;
                else if (op == 2)
                    executionCore.R(rd).Value = (uint)(rsV + rn);

                if (op == 0)
                {
                    var opA = (long)rsV & 0xFFFFFFFF;
                    var opB = (long)rnV & 0xFFFFFFFF;
                    var result = (ulong)(opA + opB);
                    var signedResult = rsV + rnV;

                    executionCore.CurrentProgramStatusRegister.Signed = (signedResult & 0x80000000u) == 0x80000000u;
                    executionCore.CurrentProgramStatusRegister.Zero = signedResult == 0;
                    //executionCore.CurrentProgramStatusRegister.Overflow = result > uint.MaxValue;
                    executionCore.CurrentProgramStatusRegister.Carry = (result & 0x100000000u) == 0x100000000u;
                }
                else if (op == 2)
                {
                    var opA = (long)rsV & 0xFFFFFFFF;
                    var opB = (long)rn & 0xFFFFFFFF;
                    var result = (ulong)(opA + opB);
                    var signedResult = rsV + rn;
                    var signedLongResult = opA + opB;

                    executionCore.CurrentProgramStatusRegister.Signed = (signedResult & 0x80000000u) == 0x80000000u;
                    executionCore.CurrentProgramStatusRegister.Zero = signedResult == 0;
                    //executionCore.CurrentProgramStatusRegister.Overflow = signedLongResult > uint.MaxValue;
                    executionCore.CurrentProgramStatusRegister.Carry = (result & 0x100000000u) == 0x100000000u;
                }       
            }
            else if ((opcode & 0xF800u) >> 11 == 6)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = opcode & 0xFFu;

                var rdV = executionCore.R(rd).Value;
                var rnV = nn;

                var opA = (long)rdV & 0xFFFFFFFF;
                var opB = (long)rnV & 0xFFFFFFFF;
                var result = (ulong)(opA + opB);
                var signedResult = rdV + rnV;

                executionCore.CurrentProgramStatusRegister.Signed = (signedResult & 0x80000000u) == 0x80000000u;
                executionCore.CurrentProgramStatusRegister.Zero = signedResult == 0;
                executionCore.CurrentProgramStatusRegister.Overflow = result > uint.MaxValue;
                executionCore.CurrentProgramStatusRegister.Carry = (result & 0x100000000u) == 0x100000000u;

                executionCore.R(rd).Value = signedResult;
            }
            else if ((opcode & 0xFF00u) == 0xB000u)
            {
                var positive = (opcode & 0x80u) == 0;
                var nn = (opcode & 0x7Fu) * 4;

                if (positive)
                    executionCore.R(13).Value = executionCore.R(13).Value + nn;
                else
                    executionCore.R(13).Value = executionCore.R(13).Value - nn;
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
            else if ((opcode & 0xFF00u) == 0xB000u)
            {
                var positive = (opcode & 0x80u) == 0;
                var nn = (opcode & 0x7Fu) * 4;

                if (positive)
                    return string.Format("add #sp, 0x{0:x}", nn);
                else
                    return string.Format("add #sp, -0x{0:x}", nn);
            }

            throw new NotSupportedException();
        } 
    }
}
