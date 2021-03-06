﻿using System;
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
            if ((opcode & 0xF000u) == 0x9000u)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = (opcode & 0xFFu) * 4;

                var address = ((uint)((int)executionCore.R(13).Value + (int)nn));
                executionCore.memoryManager.GetMemoryRegionForAddress(address).Write32(address, executionCore.R(rd).Value);

                return false;
            }
            else if ((opcode & 0xF000u) == 0x8000u)
            {
                var nn = (opcode & 0x7C0u) >> 6;
                var rb = (opcode & 0x38u) >> 3;
                var rd = opcode & 0x7u;

                var address = ((uint)((int)executionCore.R(rb).Value + (int)nn));
                executionCore.memoryManager.GetMemoryRegionForAddress(address).Write16(address, (ushort)executionCore.R(rd).Value);

                return false;
            }
            else if ((opcode & 0xF800u) == 0x6000u)
            {
                return false;
            }
            else
            {
                var rd = opcode & 0x7u;
                var rb = (opcode & 0x38u) >> 3;
                var ro = (opcode & 0x1C0u) >> 6;

                var address = (uint)((int)executionCore.R(rb).Value + (int)executionCore.R(ro).Value);
                executionCore.memoryManager.GetMemoryRegionForAddress(address).Write32(address, executionCore.R(rd).Value);

                return false;
            }
        }

        public override string InstructionAsString(ushort opcode)
        {
            if ((opcode & 0xF000u) == 0x9000u)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = (opcode & 0xFFu) * 4;

                return string.Format("str r{0}, [#sp, 0x{1:X}]", rd, nn);
            }
            else if ((opcode & 0xF000u) == 0x8000u)
            {
                var nn = (opcode & 0x7C0u) >> 6;
                var rb = (opcode & 0x38u) >> 3;
                var rd = opcode & 0x7u;

                return string.Format("strh r{0}, [r{1}, 0x{2:X}]", rd, rb, nn);
            }
            else
            {
                var rd = opcode & 0x7u;
                var rb = (opcode & 0x38u) >> 3;
                var ro = (opcode & 0x1C0u) >> 6;

                return string.Format("str r{0}, [r{1}, r{2}]", rd, (opcode & 0x4800u) == 0x4800u ? "pc" : rb.ToString(), ro);
            }
        }
    }
}
