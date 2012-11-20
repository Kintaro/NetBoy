using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ThumbCondition : uint
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
    public static class ThumbConditionDecoder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public static ThumbCondition Decode(uint opcode)
        {
            return (ThumbCondition)opcode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string ToString(ThumbCondition condition)
        {
            switch (condition)
            {
                case ThumbCondition.Equals: return "eq";
                case ThumbCondition.NotEquals: return "ne";
                case ThumbCondition.UnsignedHigherOrSame: return "hs";
                case ThumbCondition.UnsignedLower: return "lo";
                case ThumbCondition.Negative: return "mi";
                case ThumbCondition.Positive: return "pl";
                case ThumbCondition.Overflow: return "vs";
                case ThumbCondition.NoOverflow: return "vc";
                case ThumbCondition.UnsignedHigher: return "hi";
                case ThumbCondition.UnsignedLowerOrSame: return "ls";
                case ThumbCondition.GreaterOrEqual: return "ge";
                case ThumbCondition.LessThan: return "lt";
                case ThumbCondition.GreaterThan: return "gt";
                case ThumbCondition.LessOrEqual: return "le";
                case ThumbCondition.Always: return "";
                case ThumbCondition.Never: return "nv";
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static bool CheckCondition(ExecutionCore executionCore, ThumbCondition condition)
        {
            var z = executionCore.CurrentProgramStatusRegister.Zero;
            var c = executionCore.CurrentProgramStatusRegister.Carry;
            var n = executionCore.CurrentProgramStatusRegister.Signed;
            var v = executionCore.CurrentProgramStatusRegister.Overflow;

            switch (condition)
            {
                case ThumbCondition.Equals: return z;
                case ThumbCondition.NotEquals: return !z;
                case ThumbCondition.UnsignedHigherOrSame: return c;
                case ThumbCondition.UnsignedLower: return !c;
                case ThumbCondition.Negative: return n;
                case ThumbCondition.Positive: return !n;
                case ThumbCondition.Overflow: return v;
                case ThumbCondition.NoOverflow: return !v;
                case ThumbCondition.UnsignedHigher: return c && !z;
                case ThumbCondition.UnsignedLowerOrSame: return !c || z;
                case ThumbCondition.GreaterOrEqual: return n == v;
                case ThumbCondition.LessThan: return n != v;
                case ThumbCondition.GreaterThan: return !z && n == v;
                case ThumbCondition.LessOrEqual: return z || n != v;
                case ThumbCondition.Always: return true;
                case ThumbCondition.Never: return false;
            }

            throw new NotSupportedException();
        }
    }
}
