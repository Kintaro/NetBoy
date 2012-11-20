using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Logical
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TeqInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);

            var rn = (opcode & 0xF0000u) >> 16;
            var rnV = executionCore.R(rn).Value;

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
                ;

            var temp = rnV ^ op2;
            executionCore.CurrentProgramStatusRegister.Zero = temp == 0;
            executionCore.CurrentProgramStatusRegister.Carry = carry;
            executionCore.CurrentProgramStatusRegister.Signed = (temp & 0x80000000u) != 0;

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = (opcode & 0xF0000u) >> 16;
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

            if (immediate)
                return string.Format("teq{0} r{1}, 0x{2:X}", ArmConditionDecoder.ToString(condition), rn, op2);
            else
                return string.Format("teq{0} r{1}, r{2}", ArmConditionDecoder.ToString(condition), rn, op2);
        }
    }
}
