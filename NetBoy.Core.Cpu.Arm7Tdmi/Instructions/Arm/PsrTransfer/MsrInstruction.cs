using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Branch;
using NetBoy.Core.Cpu.Arm7Tdmi.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.PsrTransfer
{
    /// <summary>
    /// 
    /// </summary>
    public class MsrInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);
            var isBx = (opcode & 0x0FFFFF00) == 0x12FFF00u;

            if (isBx)
            {
                var bxInstruction = new BxInstruction();
                return bxInstruction.Execute(executionCore, opcode);
            }

            var immediate = (opcode & 0x2000000u) != 0;

            var f = (opcode & 0x80000u) != 0;
            var s = (opcode & 0x40000u) != 0;
            var x = (opcode & 0x20000u) != 0;
            var c = (opcode & 0x10000u) != 0;

            var cpsr = (opcode & 0x400000u) == 0;
            var reg = (cpsr ? executionCore.CurrentProgramStatusRegister as Register : executionCore.SavedProgramStatusRegister[executionCore.currentMode] as Register);

            var op = 0u;
            if (immediate)
            {
                var shift = (opcode & 0xF00u) >> 8;
                var imm = opcode & 0xFFu;

                op = BitHelper.Ror(imm, shift * 2);
            }
            else
            {
                var rn = (opcode & 0xFu);
                op = executionCore.R(rn).Value;
            }

            if (ArmConditionDecoder.CheckCondition(executionCore, condition))
            {
                if (immediate)
                {
                    if (f) reg.Value = (reg.Value & 0x00FFFFFFu) | ((op << 24) & 0xFF000000u);
                    if (s) reg.Value = (reg.Value & 0xFF00FFFFu) | ((op << 16) & 0x00FF0000u);
                    if (x) reg.Value = (reg.Value & 0xFFFF00FFu) | ((op << 8) & 0x0000FF00u);
                    if (c) reg.Value = (reg.Value & 0xFFFFFF00u) | ((op) & 0x000000FFu);
                }
                else
                {
                    if (f) reg.Value = (reg.Value & 0x00FFFFFFu) | (op & 0xFF000000u);
                    if (s) reg.Value = (reg.Value & 0xFF00FFFFu) | (op & 0x00FF0000u);
                    if (x) reg.Value = (reg.Value & 0xFFFF00FFu) | (op & 0x0000FF00u);
                    if (c) reg.Value = (reg.Value & 0xFFFFFF00u) | (op & 0x000000FFu);
                }
                if (!immediate && ((opcode & 0xFu) != 13))
                    executionCore.R(13).Value = 0x0u;
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var isBx = (opcode & 0x0FFFFF00) == 0x12FFF00u;

            if (isBx)
            {
                var bxInstruction = new BxInstruction();
                return bxInstruction.InstructionAsString(opcode);
            }

            var condition = ArmConditionDecoder.Decode(opcode);
            var immediate = (opcode & 0x2000000u) != 0;

            var cpsr = false;
            var op = 0u;
            if (immediate)
            {
                var shift = (opcode & 0xF00u) >> 8;
                var imm = opcode & 0xFFu;

                op = BitHelper.Ror(imm, shift * 2);
            }
            else
            {
                cpsr = (opcode & 0x400000u) == 0;
                op = opcode & 0xF;
            }

            if (immediate)
                return string.Format("msr{0} #{1}, {2}", ArmConditionDecoder.ToString(condition), cpsr ? "cpsr" : "spsr", op);
            else
                return string.Format("msr{0} #{1}, #{2}", ArmConditionDecoder.ToString(condition), cpsr ? "cpsr" : "spsr", op);
        }
    }
}
