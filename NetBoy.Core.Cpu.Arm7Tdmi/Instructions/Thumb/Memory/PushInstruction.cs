using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PushInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var registerList = opcode & 0xFFu;
            var pclr = ((opcode & 0x100u) >> 8) == 1;

            var start = (uint)(executionCore.R(13).Value - (BitHelper.BitCount(registerList) + (pclr ? 1 : 0)) * 4);
            executionCore.R(13).Value = start;

            for (var i = 0; i < 8; ++i)
            {
                if ((registerList & (1u << i)) != (1u << i))
                    continue;
                executionCore.memoryManager.GetMemoryRegionForAddress(start).Write32(start, executionCore.R(i).Value);
                start = start + 8;
            }

            if (pclr)
                executionCore.memoryManager.GetMemoryRegionForAddress(start).Write32(start, executionCore.R(14).Value);

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var registerList = opcode & 0xFFu;
            var pclr = ((opcode & 0x100u) >> 8) == 1;

            var smallestRegister = -1;
            var highestRegister = -1;

            for (var i = 0; i < 8; ++i)
            {
                if ((registerList & (1u << i)) != (1u << i))
                    continue;
                if (smallestRegister < 0)
                    smallestRegister = i;
                highestRegister = i;
            }

            var registerCount = BitHelper.BitCount(registerList);

            if (registerCount == 0 && pclr)
                return string.Format("push [#lr]", smallestRegister, highestRegister);
            else if (registerCount == 1 && pclr)
                return string.Format("push [r{0}, #lr]", smallestRegister);
            else if (pclr)
                return string.Format("push [r{0}-r{1}, #lr]", smallestRegister, highestRegister);
            else if (registerCount == 1)
                return string.Format("push [r{0}]", smallestRegister);
            else
                return string.Format("push [r{0}-r{1}]", smallestRegister, highestRegister);
        }
    }
}
