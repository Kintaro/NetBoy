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
    [Flags]
    public enum ArmCondition : uint
    {
        Equals = 0,
        NotEquals = 1,
        UnsignedHigherOrSame = 2,
        UnsignedLower = 3,
        Negative = 4,
        Positive = 5,
        Overflow = 6,
        NoOverflow = 7,
        UnsignedHigher = 8,
        UnsignedLowerOrSame = 9,
        GreaterOrEqual = 10,
        LessThan = 11,
        GreaterThan = 12,
        LessOrEqual = 13,
        Always = 14,
        Never = 15,
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ArmConditionDecoder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public static ArmCondition Decode(uint opcode)
        {
            var code = (opcode & 0xE0000000u) >> 29;
            return (ArmCondition)code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string ToString(ArmCondition condition)
        {
            switch (condition)
            {
                case ArmCondition.Equals: return "eq";
                case ArmCondition.NotEquals: return "ne";
                case ArmCondition.UnsignedHigherOrSame: return "hs";
                case ArmCondition.UnsignedLower: return "lo";
                case ArmCondition.Negative: return "mi";
                case ArmCondition.Positive: return "pl";
                case ArmCondition.Overflow: return "vs";
                case ArmCondition.NoOverflow: return "";
                case ArmCondition.UnsignedHigher: return "hi";
                case ArmCondition.UnsignedLowerOrSame: return "ls";
                case ArmCondition.GreaterOrEqual: return "ge";
                case ArmCondition.LessThan: return "lt";
                case ArmCondition.GreaterThan: return "gt";
                case ArmCondition.LessOrEqual: return "le";
                case ArmCondition.Always: return "";
                case ArmCondition.Never: return "";
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static bool CheckCondition(ExecutionCore executionCore, ArmCondition condition)
        {
            var z = executionCore.CurrentProgramStatusRegister.Zero;
            var c = executionCore.CurrentProgramStatusRegister.Carry;
            var n = executionCore.CurrentProgramStatusRegister.Signed;
            var v = executionCore.CurrentProgramStatusRegister.Overflow;

            switch (condition)
            {
                case ArmCondition.Equals: return z;
                case ArmCondition.NotEquals: return !z;
                case ArmCondition.UnsignedHigherOrSame: return c;
                case ArmCondition.UnsignedLower: return !c;
                case ArmCondition.Negative: return n;
                case ArmCondition.Positive: return !n;
                case ArmCondition.Overflow: return v;
                case ArmCondition.NoOverflow: return !v;
                case ArmCondition.UnsignedHigher: return c && !z;
                case ArmCondition.UnsignedLowerOrSame: return !c || z;
                case ArmCondition.GreaterOrEqual: return n == v;
                case ArmCondition.LessThan: return n != v;
                case ArmCondition.GreaterThan: return !z && n == v;
                case ArmCondition.LessOrEqual: return z || n != v;
                case ArmCondition.Always: return true;
                case ArmCondition.Never: return false;
            }

            throw new NotSupportedException();
        }
    }
}
