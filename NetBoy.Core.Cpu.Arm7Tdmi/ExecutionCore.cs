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
        private const int SystemMode = 0;
        private const int FiqMode = 1;
        private const int SupervisorMode = 2;
        private const int AbortMode = 3;
        private const int IrqMode = 4;
        private const int UndefinedMode = 5;

        private int currentMode = 0;
        /// <summary>
        /// 
        /// </summary>
        private Register[] baseRegisters = new Register[31];

        /// <summary>
        /// 
        /// </summary>
        private int[] modeToRegister = new int[]
        {
            // system
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
            // fiq
            0, 1, 2, 3, 4, 5, 6, 7, 16, 17, 18, 19, 20, 21, 22, 15,
            // supervisor
            0, 1, 2, 3, 4, 5, 6, 7, 16, 17, 18, 19, 20, 23, 24, 15,
            // abort
            0, 1, 2, 3, 4, 5, 6, 7, 16, 17, 18, 19, 20, 25, 26, 15,
            // irq
            0, 1, 2, 3, 4, 5, 6, 7, 16, 17, 18, 19, 20, 27, 28, 15,
            // undefined
            0, 1, 2, 3, 4, 5, 6, 7, 16, 17, 18, 19, 20, 29, 30, 15,
        };

        /// <summary>
        /// 
        /// </summary>
        public CurrentProgramStatusRegister CurrentProgramStatusRegister;
        /// <summary>
        /// 
        /// </summary>
        public SavedProgramStatusRegister[] SavedProgramStatusRegister = new SavedProgramStatusRegister[6];

        /// <summary>
        /// 
        /// </summary>
        private ThumbInstructionInstantiator thumbInstructionInstantiator = new ThumbInstructionInstantiator();

        /// <summary>
        /// 
        /// </summary>
        public ExecutionCore()
        {
            for (var i = 0; i < 31; ++i)
                this.baseRegisters[i] = new Register();
            for (var i = 0; i < 6; ++i)
                this.SavedProgramStatusRegister[i] = new SavedProgramStatusRegister();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerNumber"></param>
        /// <returns></returns>
        public Register R(uint registerNumber)
        {
            return this.baseRegisters[this.modeToRegister[this.currentMode * 16 + registerNumber]];
        }

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
