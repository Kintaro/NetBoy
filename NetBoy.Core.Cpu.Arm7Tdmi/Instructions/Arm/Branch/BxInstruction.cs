using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Branch
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BxInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = opcode & 0xFu;

            var thumb = (executionCore.R(rn).Value & 0x1) == 1;
            var rnV = executionCore.R(rn).Value;

            if (thumb)
                rnV = rnV + 1;

            if (ArmConditionDecoder.CheckCondition(executionCore, condition) &&
                (opcode & 0xFFF00u) == 0xFFF00u &&
                (opcode & 0xF0u) >> 4 == 0x1u)
            {
                executionCore.CurrentProgramStatusRegister.ThumbMode = thumb;
                executionCore.PC.Value = rnV;
                executionCore.R(rn).Value = 0;
                return true;
            }
            else if (ArmConditionDecoder.CheckCondition(executionCore, condition) &&
                (opcode & 0xFFF00u) == 0xFFF00u &&
                (opcode & 0xF0u) >> 4 == 0x3u)
            {
                executionCore.CurrentProgramStatusRegister.ThumbMode = thumb;
                executionCore.R(14).Value = executionCore.PC.Value + 4;
                executionCore.PC.Value = rnV;
                executionCore.R(rn).Value = 0;
                return true;
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = opcode & 0xFu;

            return string.Format("bx{0} #{1}", ArmConditionDecoder.ToString(condition), rn);
        }
    }
}
