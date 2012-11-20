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
        private const int UserMode = 0;

        /// <summary>
        /// 
        /// </summary>
        internal MemoryManager memoryManager;
        /// <summary>
        /// 
        /// </summary>
        internal int currentMode
        {
            get
            {
                var mode = this.CurrentProgramStatusRegister.Value & 0xFu;
                switch (mode)
                {
                    case 0: return UserMode;
                    case 1: return FiqMode;
                    case 2: return IrqMode;
                    case 3: return SupervisorMode;
                    case 7: return AbortMode;
                    case 11: return UndefinedMode;
                    case 15: return SystemMode;
                }

                throw new NotSupportedException();
            }
        }
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
        public Register PC { get { return this.baseRegisters[this.modeToRegister[currentMode * 16 + 15]]; } }

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
        /// <param name="registerNumber"></param>
        /// <returns></returns>
        public Register R(int registerNumber)
        {
            return this.baseRegisters[this.modeToRegister[this.currentMode * 16 + registerNumber]];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcode"></param>
        public bool Execute(uint opcode)
        {
            var thumbOpcode = (ushort)opcode;
            var armOpcode = opcode;

            if (this.CurrentProgramStatusRegister.ThumbMode)
            {
                var high = (thumbOpcode & 0xF000u) >> 12;
                var low = (thumbOpcode & 0x0F00u) >> 8;
                Console.WriteLine("[{7,-10}] 0x" + this.PC.Value.ToString("X8") + "> (0x" + opcode.ToString("X4") + ") " + this.thumbInstructionInstantiator.Instructions[high][low].InstructionAsString(thumbOpcode) + " [{0}{1}{2}{3}|{4}{5}{6}]", 
                    this.CurrentProgramStatusRegister.Signed ? "N" : " ",
                    this.CurrentProgramStatusRegister.Zero ? "Z" : " ",
                    this.CurrentProgramStatusRegister.Overflow ? "V" : " ",
                    this.CurrentProgramStatusRegister.Carry ? "C" : " ",
                    this.CurrentProgramStatusRegister.ThumbMode ? "T" : " ",
                    this.CurrentProgramStatusRegister.IrqDisable ? "I" : " ",
                    this.CurrentProgramStatusRegister.FiqDisable ? "F" : " ",
                    this.ModeAsString
                    );

                return this.thumbInstructionInstantiator.Instructions[high][low].Execute(this, thumbOpcode);
            }
            else
            {
                var high = (armOpcode & 0xF000000u) >> 24;
                var low = (armOpcode & 0x0F00000u) >> 20;
                Console.WriteLine("[{7,-10}] 0x" + this.PC.Value.ToString("X8") + "> (0x" + opcode.ToString("X8") + ") " + this.armInstructionInstantiator.Instructions[high][low].InstructionAsString(armOpcode) + " [{0}{1}{2}{3}|{4}{5}{6}]",
                    this.CurrentProgramStatusRegister.Signed ? "N" : " ",
                    this.CurrentProgramStatusRegister.Zero ? "Z" : " ",
                    this.CurrentProgramStatusRegister.Overflow ? "V" : " ",
                    this.CurrentProgramStatusRegister.Carry ? "C" : " ",
                    this.CurrentProgramStatusRegister.ThumbMode ? "T" : " ",
                    this.CurrentProgramStatusRegister.IrqDisable ? "I" : " ",
                    this.CurrentProgramStatusRegister.FiqDisable ? "F" : " ",
                    this.ModeAsString
                    );

                return this.armInstructionInstantiator.Instructions[high][low].Execute(this, armOpcode);
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

            var value = 0u;

            if (this.CurrentProgramStatusRegister.ThumbMode)
                value = region.Read16(pc);
            else
                value = region.Read32(pc);

            if (!this.Execute(value))
                Step();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Step()
        {
            if (this.CurrentProgramStatusRegister.ThumbMode)
                this.PC.Value = this.PC.Value + 2;
            else
                this.PC.Value = this.PC.Value + 4;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            while (true)
            {
                this.ExecuteCurrentInstruction();
                //Console.ReadKey();
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

        /// <summary>
        /// 
        /// </summary>
        public string ModeAsString
        {
            get
            {
                var mode = this.CurrentProgramStatusRegister.Value & 0xFu;
                switch (mode)
                {
                    case  0: return "User";
                    case  1: return "Fiq";
                    case  2: return "Irq";
                    case  3: return "Supervisor";
                    case  7: return "Abort";
                    case 11: return "Undefined";
                    case 15: return "System";
                }
                throw new NotSupportedException();
            }
        }
    }
}
