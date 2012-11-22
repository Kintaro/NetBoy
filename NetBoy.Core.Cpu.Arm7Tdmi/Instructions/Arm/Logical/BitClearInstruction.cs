using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Logical
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BitClearInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);

            var rd = DataProcessing.RdOperand(opcode);
            var rn = DataProcessing.RnOperand(opcode);
            var op2 = DataProcessing.RetrieveSecondOperand(executionCore, opcode);

            if (!ArmConditionDecoder.CheckCondition(executionCore, condition))
                return false;

            executionCore.R(rd).Value = executionCore.R(rn).Value & ~op2;

            if (DataProcessing.SetsConditionCodes(opcode))
            {
                var carry = DataProcessing.DoesCreateCarry(opcode);
                executionCore.CurrentProgramStatusRegister.Carry = !carry;
                executionCore.CurrentProgramStatusRegister.Zero = executionCore.R(rd).Value != 0;
                executionCore.CurrentProgramStatusRegister.Signed = (executionCore.R(rd).Value & 0x80000000u) != 0;
            }

            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);

            var rd = DataProcessing.RdOperand(opcode);
            var rn = DataProcessing.RnOperand(opcode);

            if (DataProcessing.HasImmediateOperand(opcode))
            {
                var shiftAmount = DataProcessing.ImmediateShiftAmount(opcode);
                var immediate = DataProcessing.OperandImmediate(opcode);

                return string.Format("bic{0} r{1}, r{2}, 0x{3:X}", ArmConditionDecoder.ToString(condition), rd, rn, BitHelper.Ror(immediate, shiftAmount * 2));
            }
            else
            {
                var rm = DataProcessing.OperandRegister(opcode);

                if (DataProcessing.DoesShiftByRegister(opcode))
                {
                    var rs = DataProcessing.ImmediateShiftAmount(opcode);
                    return string.Format("bic{0} r{1}, r{2}, r{3}, {4} r{5}", ArmConditionDecoder.ToString(condition), rd, rn, rm, BitHelper.ShiftTypeAsString(DataProcessing.ShiftType(opcode)), rs);
                }
                else
                {
                    var rs = DataProcessing.ShiftAmount(opcode);
                    return string.Format("bic{0} r{1}, r{2}, r{3}, {4} 0x{5:X}", ArmConditionDecoder.ToString(condition), rd, rn, rm, BitHelper.ShiftTypeAsString(DataProcessing.ShiftType(opcode)), rs);
                }
            }
        }
    }
}
