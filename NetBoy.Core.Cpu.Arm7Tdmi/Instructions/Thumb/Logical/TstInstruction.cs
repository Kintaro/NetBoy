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

            executionCore.CurrentProgramStatusRegister.Zero = value == 0;
            executionCore.CurrentProgramStatusRegister.Signed = (value & 0x80000000u) != 0;

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rs = (opcode & 0x38u) >> 3;

            return string.Format("tst r{0}, r{1}", rd, rs);
        }
    }
}
