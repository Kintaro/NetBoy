using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetBoy.Core.Memory;
using NetBoy.Core.Cpu.Arm7Tdmi;

namespace NetBoy.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EngineCore
    {
        /// <summary>
        /// 
        /// </summary>
        private MemoryManager memoryManager;
        /// <summary>
        /// 
        /// </summary>
        private ExecutionCore executionCore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void LoadBios(string path)
        {
            var reader = new BinaryReader(new FileStream(path, FileMode.Open));
            this.memoryManager.InternalMemory.Memory.SetMemory(0, reader.ReadBytes((int)reader.BaseStream.Length));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run(string pathToBios, string pathToRom)
        {
            this.memoryManager = new MemoryManager();
            this.executionCore = new ExecutionCore(this.memoryManager);

            this.LoadBios(pathToBios);
            this.executionCore.PC.Value = 0x0u;
            this.executionCore.Run();
        }
    }
}
