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
        public MemoryRegion Memory = new MemoryRegion(ExternalMemoryStart, ExternalMemoryEnd);

        public ExternalMemory()
            : base(ExternalMemoryStart, ExternalMemoryEnd)
        {
            this.Memory.Allocate();
        }

        public override MemoryRegion GetMemoryRegionForAddress(uint address)
        {
            if (this.Memory.IsAddressWithinMemory(address))
                return this.Memory;

            throw new NotSupportedException();
        }
    }
}
