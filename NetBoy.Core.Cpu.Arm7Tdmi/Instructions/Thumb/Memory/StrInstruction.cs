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
    public sealed class StrInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rb = (opcode & 0x38u) >> 3;
            var ro = (opcode & 0x1C0u) >> 6;

            var address = (uint)((int)executionCore.R(rb).Value + (int)executionCore.R(ro).Value);
            executionCore.memoryManager.GetMemoryRegionForAddress(address).Write32(address, executionCore.R(rd).Value);

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rb = (opcode & 0x38u) >> 3;
            var ro = (opcode & 0x1C0u) >> 6;

            return string.Format("str #{0}, [#{1}, #{2}]", rd, (opcode & 0x4800u) == 0x4800u ? "pc" : rb.ToString(), ro);
        }
    }
}
