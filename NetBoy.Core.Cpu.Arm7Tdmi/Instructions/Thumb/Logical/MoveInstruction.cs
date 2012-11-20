using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Logical
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MoveInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            // Move an immediate value into the destination register
            if ((opcode & 0x2000u) == 0x2000u)
            {
                var rd = ((opcode & 0x700u) >> 8);
                var imm = (opcode & 0xFFu);

                executionCore.R(rd).Value = imm;
                executionCore.CurrentProgramStatusRegister.Zero = imm == 0;
            }
            // Move source register into destination register
            else if ((opcode & 0x400u) == 0x400u)
            {
                var rd = (opcode & 0x7u);
                var rs = ((opcode & 0x38u) >> 3);

                executionCore.R(rd).Value = executionCore.R(rs).Value;
                executionCore.CurrentProgramStatusRegister.Zero = executionCore.R(rd).Value == 0;
            }
            // Move source register into destination register
            // and reset the carry and overflow flags
            else if ((opcode & 0x1800u) == 0x1800u)
            {
                var rd = (opcode & 0x7u);
                var rs = ((opcode & 0x38u) >> 3);

                executionCore.R(rd).Value = executionCore.R(rs).Value;
                executionCore.CurrentProgramStatusRegister.Carry = false;
                executionCore.CurrentProgramStatusRegister.Overflow = false;
            }

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            // Move an immediate value into the destination register
            if ((opcode & 0x2000u) == 0x2000u)
            {
                var rd = (opcode & 0x700u) >> 8;
                var imm = opcode & 0xFFu;

                return string.Format("mov r{0}, 0x{1:X}", rd, imm);
            }
            // Move source register into destination register
            else if ((opcode & 0x400u) == 0x400u)
            {
                var rd = opcode & 0x7u;
                var rs = (opcode & 0x38u) >> 3;

                return "mov #" + rd + ", #" + rs;
            }
            // Move source register into destination register
            // and reset the carry and overflow flags
            else if ((opcode & 0x1800u) == 0x1800u)
            {
                var rd = opcode & 0x7u;
                var rs = (opcode & 0x38u) >> 3;

                return "mov #" + rd + ", #" + rs;
            }

            throw new NotSupportedException();
        }
    }
}
