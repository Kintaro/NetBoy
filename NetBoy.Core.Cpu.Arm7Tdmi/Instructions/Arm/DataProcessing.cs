using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataProcessing
    {
        public static bool HasImmediateOperand(uint opcode)
        {
            return (opcode & 0x2000000u) != 0;
        }

        public static bool SetsConditionCodes(uint opcode)
        {
            return (opcode & 0x100000u) != 0;
        }

        public static uint RdOperand(uint opcode)
        {
            return (opcode & 0xF000u) >> 12;
        }

        public static uint RnOperand(uint opcode)
        {
            return (opcode & 0xF0000u) >> 16;
        }

        public static uint RnOperandValue(ExecutionCore executionCore, uint opcode)
        {
            if (RnOperand(opcode) < 0xFu)
                return executionCore.R(RnOperand(opcode)).Value;
            if (!HasImmediateOperand(opcode) && DoesShiftByRegister(opcode))
                return executionCore.R(RnOperand(opcode)).Value + 12;
            else
                return executionCore.R(RnOperand(opcode)).Value + 8;
        }

        public static uint RetrieveSecondOperand(ExecutionCore executionCore, uint opcode)
        {
            var registerAsSecondOperand = !HasImmediateOperand(opcode);

            if (registerAsSecondOperand)
            {
                var shiftByRegister = DoesShiftByRegister(opcode);

                if (shiftByRegister)
                {
                    var op2 = executionCore.R(OperandRegister(opcode)).Value;
                    var shiftAmount = executionCore.R(ImmediateShiftAmount(opcode)).Value;
                    return BitHelper.ShiftByType(op2, shiftAmount, ShiftType(opcode));
                }
                else
                {
                    var shiftAmount = ShiftAmount(opcode);
                    var op2 = executionCore.R(OperandRegister(opcode)).Value;
                    return BitHelper.ShiftByType(op2, shiftAmount, ShiftType(opcode));
                }
            }
            else
            {
                var shiftAmount = ImmediateShiftAmount(opcode);
                var immediate = OperandImmediate(opcode);

                return BitHelper.Ror(immediate, shiftAmount * 2);
            }
        }

        public static bool DoesCreateCarry(uint opcode)
        {
            var registerAsSecondOperand = HasImmediateOperand(opcode);

            if (registerAsSecondOperand)
            {
                return false;
            }
            else
            {
                var shiftAmount = ImmediateShiftAmount(opcode);
                var immediate = OperandImmediate(opcode);
                return BitHelper.CarryRor(immediate, shiftAmount * 2);
            }
        }

        public static uint ShiftType(uint opcode)
        {
            return (opcode & 0x60u) >> 5;
        }

        public static bool DoesShiftByRegister(uint opcode)
        {
            return ((opcode & 0x10u) >> 4) != 0;
        }

        public static uint ShiftAmount(uint opcode)
        {
            return (opcode & 0xF80u) >> 7;
        }

        public static uint ImmediateShiftAmount(uint opcode)
        {
            return (opcode & 0xF00u) >> 8;
        }

        public static uint OperandRegister(uint opcode)
        {
            return opcode & 0xFu;
        }

        public static uint OperandImmediate(uint opcode)
        {
            return opcode & 0xFFu;
        }
    }
}
