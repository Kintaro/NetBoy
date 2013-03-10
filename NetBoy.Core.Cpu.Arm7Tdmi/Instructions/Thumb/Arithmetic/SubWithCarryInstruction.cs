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
    public sealed class SubWithCarryInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var rs = (opcode & 0x38) >> 0x3;
            var rd = opcode & 0x7;

            var carry = executionCore.CurrentProgramStatusRegister.Carry ? 0 : 1;

            var rsV = executionCore.R(rs).Value;
            var rdV = executionCore.R(rd).Value;

            executionCore.R(rd).Value = (uint)(rdV - rsV - carry);

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var rs = (opcode & 0x38) >> 0x3;
            var rd = opcode & 0x7;

            return string.Format("sbc #{0}, #{1}", rs, rd);
        }
    }
}
