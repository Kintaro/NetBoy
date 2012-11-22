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
    public sealed class TstInstruction : ArmInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            var condition = ArmConditionDecoder.Decode(opcode);

            var rn = DataProcessing.RnOperandValue(executionCore, opcode);
            var op2 = DataProcessing.RetrieveSecondOperand(executionCore, opcode);

            if (!ArmConditionDecoder.CheckCondition(executionCore, condition))
                return false;

            var temp = (uint)(rn & op2);

            if (DataProcessing.SetsConditionCodes(opcode))
            {
                var carry = DataProcessing.DoesCreateCarry(opcode);
                executionCore.CurrentProgramStatusRegister.Carry = carry;
                executionCore.CurrentProgramStatusRegister.Zero = temp == 0;
                executionCore.CurrentProgramStatusRegister.Signed = (temp & 0x80000000u) != 0;
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

                return string.Format("tst{0} r{1}, 0x{2:X}", ArmConditionDecoder.ToString(condition), rn, BitHelper.Ror(immediate, shiftAmount * 2));
            }
            else
            {
                var rm = DataProcessing.OperandRegister(opcode);

                if (DataProcessing.DoesShiftByRegister(opcode))
                {
                    var rs = DataProcessing.ImmediateShiftAmount(opcode);
                    return string.Format("tst{0} r{1}, r{2}, {3} r{4}", ArmConditionDecoder.ToString(condition), rn, rm, BitHelper.ShiftTypeAsString(DataProcessing.ShiftType(opcode)), rs);
                }
                else
                {
                    var rs = DataProcessing.ShiftAmount(opcode);
                    return string.Format("tst{0} r{1}, r{2}, {3} 0x{4:X}", ArmConditionDecoder.ToString(condition), rn, rm, BitHelper.ShiftTypeAsString(DataProcessing.ShiftType(opcode)), rs);
                }
            }
        }
    }
}
