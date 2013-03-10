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
    public sealed class AddWithCarryInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var rs = (opcode & 0x38) >> 0x3;
            var rd = opcode & 0x7;

            var carry = executionCore.CurrentProgramStatusRegister.Carry ? 1 : 0;

            var rsV = executionCore.R(rs).Value;
            var rdV = executionCore.R(rd).Value;

            executionCore.R(rd).Value = (uint)(rdV + rsV + carry);

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var rs = (opcode & 0x38) >> 0x3;
            var rd = opcode & 0x7;

            return string.Format("adc #{0}, #{1}", rs, rd);
        }
    }
}
