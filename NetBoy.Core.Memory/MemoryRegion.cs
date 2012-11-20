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
    public class MemoryRegion
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
        private byte[] memory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public MemoryRegion(uint start, uint end)
        {
            this.memoryStart = start;
            this.memoryEnd = end;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Allocate()
        {
            this.memory = new byte[this.memoryEnd - this.memoryStart];
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
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[uint index]
        {
            get { return this.memory[index - this.memoryStart]; }
            set { this.memory[index - this.memoryStart] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint Read32(uint index)
        {
            return BitConverter.ToUInt32(this.memory, (int)(index - this.memoryStart));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ushort Read16(uint index)
        {
            return BitConverter.ToUInt16(this.memory, (int)(index - this.memoryStart));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte Read8(uint index)
        {
            return this[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Write32(uint index, uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            this[index + 0] = bytes[0];
            this[index + 1] = bytes[1];
            this[index + 2] = bytes[2];
            this[index + 3] = bytes[3];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Write16(uint index, ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            this[index + 0] = bytes[0];
            this[index + 1] = bytes[1];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Write8(uint index, byte value)
        {
            this[index + 0] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void SetMemory(byte[] p)
        {
            this.memory = p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="p"></param>
        public void SetMemory(int start, byte[] p)
        {
            p.CopyTo(this.memory, start);
        }
    }
}
