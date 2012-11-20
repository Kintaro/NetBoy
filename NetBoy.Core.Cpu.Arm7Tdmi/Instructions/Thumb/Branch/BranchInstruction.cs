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
            var conditionCode = (opcode & 0xF00u) >> 8;
            var offset = (sbyte)(opcode & 0xFFu);

            var condition = ThumbConditionDecoder.Decode(conditionCode);
            
            if (ThumbConditionDecoder.CheckCondition(executionCore, condition))
            {
                executionCore.PC.Value = (uint)(executionCore.PC.Value + offset);
                return true;
            }

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var conditionCode = (opcode & 0xF00u) >> 8;
            var offset = (int)(opcode & 0xFFu);
            var condition = ThumbConditionDecoder.Decode(conditionCode);

            return string.Format("b{0} $ + 4 + 0x{1:X}", ThumbConditionDecoder.ToString(condition), offset);
        }
    }
}
