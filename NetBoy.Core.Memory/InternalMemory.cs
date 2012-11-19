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
        public const int InternalMemoryStart = 0x0;
        public const int InternalMemoryEnd = 0x40003FE;

        public const int BiosStart = 0x0;
        public const int BiosEnd = 0x3FFF;
        public MemoryRegion Bios = new MemoryRegion(BiosStart, BiosEnd);

        public const int OnboardWorkRamStart = 0x2000000;
        public const int OnboardWorkRamEnd = 0x203FFFF;
        public MemoryRegion OnboardWorkRam = new MemoryRegion(OnboardWorkRamStart, OnboardWorkRamEnd);

        public const int InChipWorkRamStart = 0x3000000;
        public const int InChipWorkRamEnd = 0x3007FFF;
        public MemoryRegion InChipWorkRam = new MemoryRegion(InChipWorkRamStart, InChipWorkRamEnd);

        public const int IORegistersStart = 0x4000000;
        public const int IORegistersEnd = 0x40003FE;
        public MemoryRegion IORegisters = new MemoryRegion(IORegistersStart, IORegistersEnd);

        public InternalMemory()
            : base(InternalMemoryStart, InternalMemoryEnd)
        {
        }

        public override MemoryRegion GetMemoryRegionForAddress(uint address)
        {
            if (this.Bios.IsAddressWithinMemory(address))
                return this.Bios;
            else if (this.OnboardWorkRam.IsAddressWithinMemory(address))
                return this.OnboardWorkRam;
            else if (this.InChipWorkRam.IsAddressWithinMemory(address))
                return this.InChipWorkRam;

            throw new NotSupportedException();
        }
    }
}
