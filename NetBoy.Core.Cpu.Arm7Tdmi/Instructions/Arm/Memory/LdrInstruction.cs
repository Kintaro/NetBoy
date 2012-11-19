﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class LdrInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = (opcode & 0xF0000u) >> 16;
            var rd = (opcode & 0x0F000u) >> 12;

            if (ArmConditionDecoder.CheckCondition(executionCore, condition))
            {
                var offset = opcode & 0xFFFu;
                var address = executionCore.R(rn).Value + offset;
                executionCore.R(rd).Value = executionCore.memoryManager.GetMemoryForAddress(address).GetMemoryRegionForAddress(address).Read32(address);
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = (opcode & 0xF0000u) >> 16;
            var rd = (opcode & 0x0F000u) >> 12;
            var offset = opcode & 0xFFFu;

            return string.Format("ldr{0} #{1}, [#{2}, {3:X}]", ArmConditionDecoder.ToString(condition), rd, rn, offset);
        }
    }
}
