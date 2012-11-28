using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StmInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var pre = (opcode & 0x1000000u) != 0;
            var up = (opcode & 0x0800000u) != 0;
            var psr = (opcode & 0x0400000u) != 0;
            var write = (opcode & 0x0200000u) != 0;
            var load = (opcode & 0x0100000u) != 0;

            var rn = (opcode & 0xF0000u) >> 16;
            var registerList = (opcode & 0xFFFFu);

            var start = executionCore.R(rn).Value;

            for (var i = 0; i < 16; ++i)
            {
                if ((registerList & (1u << i)) != (1u << i))
                    continue;

                if (pre)
                {
                    start = start + (uint)(up ? 8 : -8);
                    executionCore.memoryManager.GetMemoryRegionForAddress(start).Write32(start, executionCore.R(i).Value);
                }
                else
                {
                    executionCore.memoryManager.GetMemoryRegionForAddress(start).Write32(start, executionCore.R(i).Value);
                    start = start + (uint)(up ? 8 : -8);
                }

                if (write)
                    executionCore.R(rn).Value = start;
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
 	        var condition = ArmConditionDecoder.Decode(opcode);
            var pre = (opcode & 0x1000000u) != 0;
            var up = (opcode & 0x0800000u) != 0;
            var psr = (opcode & 0x0400000u) != 0;
            var write = (opcode & 0x0200000u) != 0;
            var load = (opcode & 0x0100000u) != 0;

            var rn = (opcode & 0xF0000u) >> 16;
            var registerList = (opcode & 0xFFFFu);

            var baseInstruction = "stm" + (up ? "i" : "d") + (pre ? "b" : "a");

            var smallestRegister = -1;
            var highestRegister = -1;

            for (var i = 0; i < 16; ++i)
            {
                if ((registerList & (1u << i)) != (1u << i))
                    continue;
                if (smallestRegister < 0)
                    smallestRegister = i;
                highestRegister = i;
            }

            var registerCount = BitHelper.BitCount(registerList);

            if (registerCount == 1)
                return string.Format("{0} r{1}, [r{2}]", baseInstruction, rn, smallestRegister);
            else
                return string.Format("{0} r{1}, [r{2}-r{3}]", baseInstruction, rn, smallestRegister, highestRegister);
        }
    }
}
