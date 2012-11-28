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
    public sealed class LdrInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rb = (opcode & 0x38u) >> 3;
            var offset = (opcode & 0x7C0u) >> 6;

            if ((opcode & 0x4800u) == 0x4800u)
            {
                rd = (opcode & 0x700u) >> 8;
                rb = 15;
                offset = opcode & 0xFFu;
                offset = offset * 4;
            }

            var address = rb == 15 ? ((executionCore.PC.Value + 4) & ~2u) + offset : executionCore.R(rb).Value + offset;

            executionCore.R(rd).Value = executionCore.memoryManager.GetMemoryRegionForAddress(address).Read32(address);

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var rd = opcode & 0x7u;
            var rb = (opcode & 0x38u) >> 3;
            var offset = (opcode & 0x7C0u) >> 6;

            if ((opcode & 0x4800u) == 0x4800u)
            {
                rd = (opcode & 0x700u) >> 8;
                rb = 15;
                offset = opcode & 0xFFu;
                offset = offset * 4;
            }

            return string.Format("ldr r{0}, [r{1}, 0x{2:X}]", rd, (opcode & 0x4800u) == 0x4800u ? "pc" : rb.ToString(), offset);
        }
    }
}
