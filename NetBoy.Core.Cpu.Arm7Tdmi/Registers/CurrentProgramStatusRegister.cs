using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Registers
{
    /// <summary>
    ///     The current condition codes (flags) and CPU control bits are stored in the CPSR register. 
    ///     When an exception arises, the old CPSR is saved in the SPSR of the respective 
    ///     exception-mode (much like PC is saved in LR).
    /// </summary>
    public class CurrentProgramStatusRegister : Register
    {
        public const uint SignedFlag = 0x80000000;
        public const uint ZeroFlag = 0x40000000;
        public const uint CarryFlag = 0x20000000;
        public const uint OverflowFlag = 0x10000000;
        public const uint StickyOverflowFlag = 0x8000000;
        public const uint IrqDisableFlag = 0x40;
        public const uint FiqDisableFlag = 0x20;
        public const uint StateFlag = 0x10;

        public bool Signed
        {
            get { return this.IsFlagSet(SignedFlag); }
            set { this.SetFlag(SignedFlag, value); }
        }

        public bool Zero
        {
            get { return this.IsFlagSet(ZeroFlag); }
            set { this.SetFlag(ZeroFlag, value); }
        }

        public bool Carry
        {
            get { return this.IsFlagSet(CarryFlag); }
            set { this.SetFlag(CarryFlag, value); }
        }

        public bool Overflow
        {
            get { return this.IsFlagSet(OverflowFlag); }
            set { this.SetFlag(OverflowFlag, value); }
        }

        public bool StickyOverflow
        {
            get { return this.IsFlagSet(StickyOverflowFlag); }
            set { this.SetFlag(StickyOverflowFlag, value); }
        }

        public bool IrqDisable
        {
            get { return this.IsFlagSet(IrqDisableFlag); }
            set { this.SetFlag(IrqDisableFlag, value); }
        }

        public bool FiqDisable
        {
            get { return this.IsFlagSet(FiqDisableFlag); }
            set { this.SetFlag(FiqDisableFlag, value); }
        }

        public bool ArmMode
        {
            get { return !this.IsFlagSet(StateFlag); }
            set { this.SetFlag(SignedFlag, !value); }
        }

        public bool ThumbMode
        {
            get { return this.IsFlagSet(StateFlag); }
            set { this.SetFlag(StateFlag, value); }
        }

        public bool State
        {
            get { return this.IsFlagSet(StateFlag); }
            set { this.SetFlag(StateFlag, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private bool IsFlagSet(uint flag)
        {
            return (this.Value & flag) == flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="value"></param>
        private void SetFlag(uint flag, bool value)
        {
            this.Value = value ? this.Value | flag : this.Value & ~flag;
        }
    }
}
