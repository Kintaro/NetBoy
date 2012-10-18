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

        public byte[] Bios;
        public uint BiosStart = 0u;
        public uint BiosEnd = 0x3FFFu;

        public byte[] OnboardWorkRam = new byte[0x3FFFFu];
        public uint OnboardWorkRamStart = 0x2000000u;
        public uint OnboardWorkRamEnd = 0x203FFFFu;

        public byte[] InChipWorkRam = new byte[0x7FFFu];
        public uint InChipWorkRamStart = 0x3000000u;
        public uint InChipWorkRamEnd = 0x3007FFFu;

        public byte[] IORegisters = new byte[0x3FEu];
        public uint IORegistersStart = 0x4000000u;
        public uint IORegistersEnd = 0x40003FEu;

        public InternalMemory()
            : base(InternalMemoryStart, InternalMemoryEnd)
        {
        }

        public void CreateBiosMemory()
        {
            this.Bios = new byte[0x3FFF];
        }
    }
}
