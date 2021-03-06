﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Branch
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BranchLinkInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var nn = (uint)(opcode & 0xFFFFFFu);

            if (ArmConditionDecoder.CheckCondition(executionCore, condition))
            {
                var temp = executionCore.PC.Value;
                if ((nn & 0x800000u) != 0)
                    nn = nn | 0xFF000000u;
                executionCore.PC.Value = executionCore.PC.Value + 8 + nn * 4;
                executionCore.R(14).Value = temp + 4;
                return true;
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var nn = (uint)(opcode & 0xFFFFFFu);
            if ((nn & 0x800000u) != 0)
                nn = nn | 0xFF000000u;
            return string.Format("bl{1} $ + 8 + 4 * 0x{0:X}", nn, ArmConditionDecoder.ToString(condition));
        }
    }
}
