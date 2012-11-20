using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Arithmetic
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AddInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = (opcode & 0xF0000u) >> 16;
            var rd = (opcode & 0x0F000u) >> 12;
            var immediate = (opcode & 0x2000000u) != 0;
            var shift = (opcode & 0xF00u) >> 8;

            var op2 = 0u;
            var carry = false;

            if (immediate)
            {
                var nn = opcode & 0xFFu;
                var type = (opcode & 0x60u) >> 0x5;
                op2 = BitHelper.ShiftByType(nn, shift * 2, type);
                carry = BitHelper.CarryByType(nn, shift * 2, type);
            }
            else
            {

            }

            var s = (opcode & 0x100000u) != 0;

            if (ArmConditionDecoder.CheckCondition(executionCore, condition))
            {
                if (rn == 15)
                    executionCore.R(rd).Value = (uint)((int)executionCore.R(rn).Value + 8 + (int)op2);
                else
                    executionCore.R(rd).Value = (uint)((int)executionCore.R(rn).Value + (int)op2);

                if (s)
                {
                    executionCore.CurrentProgramStatusRegister.Carry = carry;
                }
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = (opcode & 0xF0000u) >> 16;
            var rd = (opcode & 0x0F000u) >> 12;
            var immediate = (opcode & 0x2000000u) != 0;
            var shift = (opcode & 0xF00u) >> 8;

            var op2 = 0u;

            if (immediate)
            {
                var nn = opcode & 0xFFu;
                var type = (opcode & 0x60u) >> 0x5;
                op2 = BitHelper.ShiftByType(nn, shift * 2, type);
            }
            else
                ;

            return string.Format("add{0} #{1}, #{2}, 0x{3:X}", ArmConditionDecoder.ToString(condition), rd, rn, op2);
        }
    }
}
