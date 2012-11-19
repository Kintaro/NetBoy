using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Logical
{
    public sealed class NopInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            return false;
        }
    }
}
