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

            if ((executionCore.R(rs).Value & 0x8u) == 0)
            {
                if ((hs & 0x1u) != 0)
                    rs = rs | 0x8u;
                var r = executionCore.R(rs).Value;
                executionCore.PC.Value = r;
                executionCore.CurrentProgramStatusRegister.ArmMode = true;
                return true;
            }

            executionCore.JumpToAddress((uint)executionCore.R(rs).Value);
            return true;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var hd = (opcode & 0x80u) >> 7;
            var hs = (opcode & 0x40u) >> 6;

            var rs = (opcode & 0x38u) >> 3;
            var rd = (opcode & 0x7u);

            if ((hs & 0x1u) != 0)
                rs = rs | 0x8u;

            return string.Format("bx #{0}", rs);
        }
    }
}
