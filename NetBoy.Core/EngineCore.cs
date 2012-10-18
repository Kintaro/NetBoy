using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetBoy.Core.Memory;

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
        private MemoryManager memoryManager = new MemoryManager();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void LoadBios(string path)
        {
            var reader = new BinaryReader(new FileStream(path, FileMode.Open));
            this.memoryManager.InternalMemory.Bios.Allocate();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run(string pathToBios, string pathToRom)
        {
            this.LoadBios(pathToBios);
        }
    }
}
