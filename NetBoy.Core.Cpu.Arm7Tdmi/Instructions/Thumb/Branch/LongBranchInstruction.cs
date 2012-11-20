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
    public sealed class LongBranchInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, ushort opcode)
        {
            var upperOpcode = (opcode & 0xF800u) >> 11;
            var nn = opcode & 0x7FFu;
            
            if (upperOpcode == 0x1Eu)
            {
                executionCore.R(14).Value = executionCore.PC.Value + 4 + (nn << 12);
                return false;
            }
            else if (upperOpcode == 0x1Fu)
            {
                executionCore.PC.Value = executionCore.R(14).Value + (nn << 1);
                executionCore.PC.Value = executionCore.PC.Value & 0x7FFFFFu;
                executionCore.R(14).Value = (executionCore.PC.Value + 2) | 0x1u; 
                return true;
            }
            else if (upperOpcode == 0x1Du)
            {
                executionCore.R(14).Value = (executionCore.PC.Value + 2) | 0x1u;
                executionCore.PC.Value = executionCore.R(14).Value + (nn << 1);
                executionCore.CurrentProgramStatusRegister.ArmMode = true;
                return true;
            }

            return false;
        }

        public override string InstructionAsString(ushort opcode)
        {
            var upperOpcode = (opcode & 0xF800u) >> 11;
            var nn = opcode & 0x7FFu;

            if (upperOpcode == 0x1Eu)
            {
                return string.Format("blh 0x{0:X}", nn);
            }
            else if (upperOpcode == 0x1Fu)
            {
                return string.Format("bll 0x{0:X}", nn);
            }
            else if (upperOpcode == 0x1Du)
            {
                return string.Format("blx 0x{0:X}", nn);
            }

            throw new NotSupportedException();
        }
    }
}
