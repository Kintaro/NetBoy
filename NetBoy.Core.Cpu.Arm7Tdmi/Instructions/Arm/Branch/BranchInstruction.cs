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
    public sealed class BranchInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var nn = (uint)(opcode & 0xFFFFFFu);

            if (ArmConditionDecoder.CheckCondition(executionCore, condition))
            {
                executionCore.PC.Value = executionCore.PC.Value + 8 + nn * 4;
                return true;
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var nn = (uint)(opcode & 0xFFFFFFu);
            return string.Format("b{1} $ + 8 + 4 * 0x{0:X}", nn, ArmConditionDecoder.ToString(condition));
        }
    }
}
