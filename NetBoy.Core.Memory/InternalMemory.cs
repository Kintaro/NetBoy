using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class InternalMemory : BaseMemory
    {
        public const uint InternalMemoryStart = 0x0u;
        public const uint InternalMemoryEnd = 0x40003FE;

        public const uint BiosStart = 0u;
        public const uint BiosEnd = 0x3FFFu;
        public MemoryRegion Bios = new MemoryRegion(BiosStart, BiosEnd);

        public const uint OnboardWorkRamStart = 0x2000000u;
        public const uint OnboardWorkRamEnd = 0x203FFFFu;
        public MemoryRegion OnboardWorkRam = new MemoryRegion(OnboardWorkRamStart, OnboardWorkRamEnd);

        public const uint InChipWorkRamStart = 0x3000000u;
        public const uint InChipWorkRamEnd = 0x3007FFFu;
        public MemoryRegion InChipWorkRam = new MemoryRegion(InChipWorkRamStart, InChipWorkRamEnd);

        public const uint IORegistersStart = 0x4000000u;
        public const uint IORegistersEnd = 0x40003FEu;
        public MemoryRegion IORegisters = new MemoryRegion(IORegistersStart, IORegistersEnd);

        public InternalMemory()
            : base(InternalMemoryStart, InternalMemoryEnd)
        {
        }
    }
}
