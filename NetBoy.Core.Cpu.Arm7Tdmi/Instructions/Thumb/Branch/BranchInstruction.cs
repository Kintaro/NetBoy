using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Branch
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BranchInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            if ((opcode & 0xF800u) == 0xE000u)
            {
                var offset = opcode & 0x7FFu;

                executionCore.PC.Value = (uint)(executionCore.PC.Value + 4 + offset * 2);
                return true;
            }
            else
            {
                var conditionCode = (opcode & 0xF00u) >> 8;
                var offset = (sbyte)(opcode & 0xFFu);

                var condition = ThumbConditionDecoder.Decode(conditionCode);

                if (ThumbConditionDecoder.CheckCondition(executionCore, condition))
                {
                    executionCore.PC.Value = (uint)(executionCore.PC.Value + 4 + offset * 2);
                    return true;
                }
            }

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            if ((opcode & 0xF800u) == 0xE000u)
            {
                var offset = opcode & 0x7FFu;
                return string.Format("b $ + 4 + 0x{0:X}", offset);
            }
            else
            {
                var conditionCode = (opcode & 0xF00u) >> 8;
                var offset = (int)(opcode & 0xFFu);
                var condition = ThumbConditionDecoder.Decode(conditionCode);

                return string.Format("b{0} $ + 4 + 0x{1:X}", ThumbConditionDecoder.ToString(condition), offset);
            }
        }
    }
}
