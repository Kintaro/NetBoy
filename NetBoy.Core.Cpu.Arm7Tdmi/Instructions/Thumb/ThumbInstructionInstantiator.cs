using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Logical;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ThumbInstructionInstantiator
    {
        /// <summary>
        ///     Most opcodes can be recognized by their first byte and are thus dividable
        ///     into a 16 by 16 table. ALU opcodes however have additional opcodes
        ///     in bits 9 - 6, and therefore this instruction here works as a
        ///     2-stage dispatcher.
        /// </summary>
        private class AluInstructionInstantiator : ThumbInstruction
        {
            // 010000----
            public ThumbInstruction[] Instructions = new ThumbInstruction[]
            {
                new AndInstruction(),
                null,
                new LogicalShiftLeftInstruction(),
                new LogicalShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                new OrInstruction(),
                null,
                null,
                new MoveNegatedInstruction(),
            };

            public override void Execute(ExecutionCore executionCore, uint opcode)
            {
                var op = (opcode & 0x3C0u) >> 6;
                this.Instructions[op].Execute(executionCore, opcode);
            }
        }

        public ThumbInstruction[][] Instructions = new ThumbInstruction[][]
        {
            // 0000
            new ThumbInstruction[]
            {
                // 0---
                new LogicalShiftLeftInstruction(),
                new LogicalShiftLeftInstruction(),
                new LogicalShiftLeftInstruction(),
                new LogicalShiftLeftInstruction(),
                new LogicalShiftLeftInstruction(),
                new LogicalShiftLeftInstruction(),
                new LogicalShiftLeftInstruction(),
                new LogicalShiftLeftInstruction(),
                // 1---
                new LogicalShiftRightInstruction(),
                new LogicalShiftRightInstruction(),
                new LogicalShiftRightInstruction(),
                new LogicalShiftRightInstruction(),
                new LogicalShiftRightInstruction(),
                new LogicalShiftRightInstruction(),
                new LogicalShiftRightInstruction(),
                new LogicalShiftRightInstruction(),
            },
            // 0001
            new ThumbInstruction[]
            {
                // 0---
                new ArithmeticShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                new ArithmeticShiftRightInstruction(),
                // 1--- (add/sub)
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            },
            // 0010
            new ThumbInstruction[]
            {
                // 0---
                new MoveInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                // 1---
            },
            // 0011
            new ThumbInstruction[]
            {
                // 0---
                null,
                // 1---
            },
            // 0100
            new ThumbInstruction[]
            {
                // 00--
                new AluInstructionInstantiator(),
                new AluInstructionInstantiator(),
                new AluInstructionInstantiator(),
                new AluInstructionInstantiator(),
                // 01--
                null,
                null,
                new MoveInstruction(),
                null,
                // 1---
            },
            // 0101
            new ThumbInstruction[]
            {
            },
            // 0110
            new ThumbInstruction[]
            {
            },
            // 0111
            new ThumbInstruction[]
            {
            },
            // 1000
            new ThumbInstruction[]
            {
            },
            // 1001
            new ThumbInstruction[]
            {
            },
            // 1010
            new ThumbInstruction[]
            {
            },
            // 1011
            new ThumbInstruction[]
            {
            },
            // 1100
            new ThumbInstruction[]
            {
            },
            // 1101
            new ThumbInstruction[]
            {
            },
            // 1110
            new ThumbInstruction[]
            {
            },
            // 1111
            new ThumbInstruction[]
            {
            },
        };
    }
}
