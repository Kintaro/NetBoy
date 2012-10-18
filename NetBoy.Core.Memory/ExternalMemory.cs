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

        public byte[] WaitState0 = new byte[0x1FFFFFFu];
        public readonly uint WaitState0Start = 0x8000000u;
        public readonly uint WaitState0End = 0x9FFFFFFu;

        public byte[] WaitState1 = new byte[0x1FFFFFFu];
        public readonly uint WaitState1Start = 0xA000000u;
        public readonly uint WaitState1End = 0xBFFFFFFu;

        public byte[] WaitState2 = new byte[0x1FFFFFFu];
        public readonly uint WaitState2Start = 0xC000000u;
        public readonly uint WaitState2End = 0xDFFFFFFu;

        public byte[] Sram = new byte[0xFFFF];
        public readonly uint SramStart = 0xE000000u;
        public readonly uint SramEnd = 0xE00FFFF;

        public ExternalMemory()
            : base(ExternalMemoryStart, ExternalMemoryEnd)
        {
        }
    }
}
