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

        public byte[] ObjPalette = new byte[0x3FFu];
        public uint ObjPaletteStart = 0x5000000u;
        public uint ObjPaletteEnd = 0x50003FFu;

        public byte[] Vram = new byte[0x6017FFFu];
        public uint VramStart = 0x6000000u;
        public uint VramEnd = 0x6017FFFu;

        public byte[] ObjAttributes = new byte[0x3FFu];
        public uint ObjAttributesStart = 0x7000000u;
        public uint ObjAttributesEnd = 0x70003FFu;

        public DisplayMemory()
            : base(DisplayMemoryStart, DisplayMemoryEnd)
        {
        }
    }
}
