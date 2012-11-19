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

            var op2 = 0u;

            if (immediate)
            {
                var nn = opcode & 0xFFu;
                op2 = nn;
            }
            else
                ;

            var temp = rnV ^ op2;
            executionCore.CurrentProgramStatusRegister.Zero = temp == 0;
            executionCore.CurrentProgramStatusRegister.Carry = true;
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
                op2 = BitHelper.Ror(nn, shift * 2);
            }
            else
                ;

            if (immediate)
                return string.Format("teq{0} #{1}, {2:X}", ArmConditionDecoder.ToString(condition), rn, op2);
            else
                return string.Format("teq{0} #{1}, #{2}", ArmConditionDecoder.ToString(condition), rn, op2);
        }
    }
}
