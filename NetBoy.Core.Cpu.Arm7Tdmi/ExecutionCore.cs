using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb;
using NetBoy.Core.Cpu.Arm7Tdmi.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutionCore
    {
        /// <summary>
        /// 
        /// </summary>
        public Register[] R = new Register[32];
        /// <summary>
        /// 
        /// </summary>
        public CurrentProgramStatusRegister CurrentProgramStatusRegister;
        /// <summary>
        /// 
        /// </summary>
        private ThumbInstructionInstantiator thumbInstructionInstantiator = new ThumbInstructionInstantiator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcode"></param>
        public void Execute(uint opcode)
        {
            if (this.CurrentProgramStatusRegister.ThumbMode)
            {
                var high = opcode >> 24;
                var low = (opcode & 0x0F00u) >> 16;
                this.thumbInstructionInstantiator.Instructions[high][low].Execute(this, opcode);
            }
        }
    }
}
