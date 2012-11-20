using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Branch;
using NetBoy.Core.Cpu.Arm7Tdmi.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.PsrTransfer
{
    /// <summary>
    /// 
    /// </summary>
    public class MrsInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);

            var cpsr = (opcode & 0x400000u) == 0;
            var reg = (cpsr ? executionCore.CurrentProgramStatusRegister as Register : executionCore.SavedProgramStatusRegister[executionCore.currentMode] as Register);
            var rd = (opcode & 0xF000u) >> 16;

            if (ArmConditionDecoder.CheckCondition(executionCore, condition))
                executionCore.R(rd).Value = reg.Value;

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var immediate = (opcode & 0x2000000u) != 0;

            var cpsr = (opcode & 0x400000u) == 0;
            var rd = (opcode & 0xF000u) >> 16;

            return string.Format("mrs{0} r{2}, r{1}", ArmConditionDecoder.ToString(condition), cpsr ? "cpsr" : "spsr", rd);
        }
    }
}
