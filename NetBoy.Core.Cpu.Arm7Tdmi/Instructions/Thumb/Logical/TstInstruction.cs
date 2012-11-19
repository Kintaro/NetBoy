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
    public sealed class TstInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            var rdV = (short)executionCore.R(rd).Value;
            var rsV = (short)executionCore.R(rs).Value;

            var value = rdV & rsV;

            executionCore.CurrentProgramStatusRegister.Value = ((uint)(executionCore.CurrentProgramStatusRegister.Value & 0x7FFFFFFFu)) | ((uint)value & 0x80000000u);

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
