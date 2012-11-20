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
        public MemoryRegion Memory = new MemoryRegion(InternalMemoryStart, InternalMemoryEnd);

        public InternalMemory()
            : base(InternalMemoryStart, InternalMemoryEnd)
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
