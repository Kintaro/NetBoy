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
    public sealed class CmpInstruction : ArmInstruction
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

            var temp = rnV - op2;
            executionCore.CurrentProgramStatusRegister.Zero = temp == 0;
            executionCore.CurrentProgramStatusRegister.Carry = !((int)rnV < (int)op2);

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);

            var rn = (opcode & 0xF0000u) >> 16;

            var immediate = (opcode & 0x2000000u) != 0;
            var op2 = 0u;

            if (immediate)
            {
                var nn = opcode & 0xFFu;
                op2 = nn;
            }
            else
                ;

            if (immediate)
                return string.Format("cmp{0} #{1}, 0x{2}", ArmConditionDecoder.ToString(condition), rn, op2);
            else
                return string.Format("cmp{0} #{1}, #{2}", ArmConditionDecoder.ToString(condition), rn, op2);
        }
    }
}
