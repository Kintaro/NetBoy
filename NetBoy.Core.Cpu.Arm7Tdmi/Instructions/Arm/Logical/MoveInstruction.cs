﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Logical
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MoveInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = (opcode & 0xF000u) >> 12;
            var immediate = (opcode & 0x2000000u) != 0;
            var shift = (opcode & 0xF00u) >> 8;

            var op2 = 0u;

            if (immediate)
            {
                var nn = opcode & 0xFFu;
                op2 = BitHelper.Ror(nn, shift * 2);
            }
            else
                ;

            executionCore.R(rn).Value = op2;
            executionCore.CurrentProgramStatusRegister.Zero = op2 != 0;
            executionCore.CurrentProgramStatusRegister.Signed = (op2 & 0x80000000u) != 0;

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var rn = (opcode & 0xF000u) >> 12;
            var immediate = (opcode & 0x2000000u) != 0;
            var shift = (opcode & 0xF00u) >> 8;

            var op2 = 0u;

            if (immediate)
            {
                var nn = opcode & 0xFFu;
                op2 = BitHelper.Ror(nn, shift * 2);
            }
            else
                ;

            if (immediate)
                return string.Format("mov{0} #{1}, {2:X}", ArmConditionDecoder.ToString(condition), rn, op2);
            else
                return string.Format("mov{0} #{1}, #{2}", ArmConditionDecoder.ToString(condition), rn, op2);
        }
    }
}
