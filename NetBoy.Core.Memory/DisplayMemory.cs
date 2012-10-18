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
    public sealed class DisplayMemory : BaseMemory
    {
        public const uint DisplayMemoryStart = 0x5000000u;
        public const uint DisplayMemoryEnd = 0x70003FFu;

        public const uint ObjPaletteStart = 0x5000000u;
        public const uint ObjPaletteEnd = 0x50003FFu;
        public MemoryRegion ObjPalette = new MemoryRegion(ObjAttributesStart, ObjAttributesEnd);

        public const uint VramStart = 0x6000000u;
        public const uint VramEnd = 0x6017FFFu;
        public MemoryRegion Vram = new MemoryRegion(VramStart, VramEnd);

        public const uint ObjAttributesStart = 0x7000000u;
        public const uint ObjAttributesEnd = 0x70003FFu;
        public MemoryRegion ObjAttributes = new MemoryRegion(ObjAttributesStart, ObjAttributesEnd);

        public DisplayMemory()
            : base(DisplayMemoryStart, DisplayMemoryEnd)
        {
        }

        public override MemoryRegion GetMemoryRegionForAddress(uint address)
        {
            if (this.ObjPalette.IsAddressWithinMemory(address))
                return this.ObjPalette;
            else if (this.Vram.IsAddressWithinMemory(address))
                return this.Vram;
            else if (this.ObjAttributes.IsAddressWithinMemory(address))
                return this.ObjAttributes;

            throw new NotSupportedException();
        }
    }
}
