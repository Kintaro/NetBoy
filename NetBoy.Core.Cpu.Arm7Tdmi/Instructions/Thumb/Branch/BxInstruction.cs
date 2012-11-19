using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Branch
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BxInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var hd = (opcode & 0x80u) >> 7;
            var hs = (opcode & 0x40u) >> 6;

            var rs = (opcode & 0x38u) >> 3;
            var rd = (opcode & 0x7u);

            executionCore.JumpToAddress((uint)executionCore.R(rs).Value);
            return true;
        }
    }
}
