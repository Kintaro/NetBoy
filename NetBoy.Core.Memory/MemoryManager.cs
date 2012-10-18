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
    public class MemoryManager
    {
        /// <summary>
        /// 
        /// </summary>
        public InternalMemory InternalMemory = new InternalMemory();
        /// <summary>
        /// 
        /// </summary>
        public ExternalMemory ExternalMemory = new ExternalMemory();
        /// <summary>
        /// 
        /// </summary>
        public DisplayMemory DisplayMemory = new DisplayMemory();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BaseMemory GetMemoryForAddress(uint address)
        {
            if (this.InternalMemory.IsAddressWithinMemory(address))
                return this.InternalMemory;
            else if (this.ExternalMemory.IsAddressWithinMemory(address))
                return this.ExternalMemory;
            else if (this.DisplayMemory.IsAddressWithinMemory(address))
                return this.DisplayMemory;

            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public MemoryRegion GetMemoryRegionForAddress(uint address)
        {
            return this.GetMemoryForAddress(address).GetMemoryRegionForAddress(address);
        }
    }
}
