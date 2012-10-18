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
    public sealed class ExternalMemory : BaseMemory
    {
        public const uint ExternalMemoryStart = 0x8000000u;
        public const uint ExternalMemoryEnd = 0xE00FFFFu;

        public const uint WaitState0Start = 0x8000000u;
        public const uint WaitState0End = 0x9FFFFFFu;
        public MemoryRegion WaitState0 = new MemoryRegion(WaitState0Start, WaitState0End);

        public const uint WaitState1Start = 0xA000000u;
        public const uint WaitState1End = 0xBFFFFFFu;
        public MemoryRegion WaitState1 = new MemoryRegion(WaitState1Start, WaitState1End);

        public const uint WaitState2Start = 0xC000000u;
        public const uint WaitState2End = 0xDFFFFFFu;
        public MemoryRegion WaitState2 = new MemoryRegion(WaitState2Start, WaitState2End);

        public const uint SramStart = 0xE000000u;
        public const uint SramEnd = 0xE00FFFF;
        public MemoryRegion Sram = new MemoryRegion(SramStart, SramEnd);

        public ExternalMemory()
            : base(ExternalMemoryStart, ExternalMemoryEnd)
        {
        }

        public override MemoryRegion GetMemoryRegionForAddress(uint address)
        {
            if (this.WaitState0.IsAddressWithinMemory(address))
                return this.WaitState0;
            else if (this.WaitState1.IsAddressWithinMemory(address))
                return this.WaitState1;
            else if (this.WaitState2.IsAddressWithinMemory(address))
                return this.WaitState2;
            else if (this.Sram.IsAddressWithinMemory(address))
                return this.Sram;

            throw new NotSupportedException();
        }
    }
}
