using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBoy.Core.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseMemory
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly uint memoryStart;
        /// <summary>
        /// 
        /// </summary>
        private readonly uint memoryEnd;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public BaseMemory(uint start, uint end)
        {
            this.memoryStart = start;
            this.memoryEnd = end;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool IsAddressWithinMemory(uint address)
        {
            return address >= this.memoryStart && address <= this.memoryEnd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public abstract MemoryRegion GetMemoryRegionForAddress(uint address);
    }
}
