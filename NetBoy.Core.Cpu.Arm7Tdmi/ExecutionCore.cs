using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetBoy.Core.Memory;
using NetBoy.Core.Cpu.Arm7Tdmi.Registers;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm;

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

        /// <summary>
        /// 
        /// </summary>
        private MemoryManager memoryManager;
        /// <summary>
        /// 
        /// </summary>
        private int currentMode = 0;
        /// <summary>
        /// 
        /// </summary>
        private Register[] baseRegisters = new Register[31];
        /// <summary>
        /// 
        /// </summary>
        private int[] modeToRegister = new int[96]
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
        public CurrentProgramStatusRegister CurrentProgramStatusRegister = new CurrentProgramStatusRegister();
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
        private ArmInstructionInstantiator armInstructionInstantiator = new ArmInstructionInstantiator();

        /// <summary>
        /// 
        /// </summary>
        public Register PC { get { return this.baseRegisters[this.modeToRegister[SystemMode * 16 + 15]]; } }

        /// <summary>
        /// 
        /// </summary>
        public ExecutionCore(MemoryManager memoryManager)
        {
            for (var i = 0; i < 31; ++i)
                this.baseRegisters[i] = new Register();
            for (var i = 0; i < 6; ++i)
                this.SavedProgramStatusRegister[i] = new SavedProgramStatusRegister();
            this.memoryManager = memoryManager;
            this.CurrentProgramStatusRegister.ThumbMode = true;
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
        public bool Execute(uint opcode)
        {
            if (this.CurrentProgramStatusRegister.ThumbMode)
            {
                var high = (opcode & 0xF000u) >> 12;
                var low  = (opcode & 0x0F00u) >>  8;
                Console.WriteLine("0x" + this.PC.Value.ToString("X") + "> (0x" + opcode.ToString("X") + ") " + this.thumbInstructionInstantiator.Instructions[high][low].InstructionAsString(opcode));
                return this.thumbInstructionInstantiator.Instructions[high][low].Execute(this, opcode);
            }
            else
            {
                var high = (opcode & 0xF0000000u) >> 28;
                var low  = (opcode & 0x0F000000u) >> 24;
                Console.WriteLine("0x" + this.PC.Value.ToString("X") + "> (0x" + opcode.ToString("X") + ") " + this.armInstructionInstantiator.Instructions[high][low].InstructionAsString(opcode));
                return this.armInstructionInstantiator.Instructions[high][low].Execute(this, opcode);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ExecuteCurrentInstruction()
        {
            var pc = this.PC.Value;
            var region = this.memoryManager.GetMemoryRegionForAddress(pc);
            var value = region.Read16(pc);

            if (!this.Execute(value))
                this.Step();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Step()
        {
            this.PC.Value = this.PC.Value + 2;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            while (true)
            {
                this.ExecuteCurrentInstruction();
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void JumpToAddress(uint p)
        {
            this.PC.Value = p;
        }
    }
}
