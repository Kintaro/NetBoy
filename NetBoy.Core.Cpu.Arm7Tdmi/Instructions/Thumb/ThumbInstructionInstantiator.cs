using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Logical;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Arithmetic;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Memory;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Branch;

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
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new OrInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new MoveNegatedInstruction(),
            };

            public override bool Execute(ExecutionCore executionCore, ushort opcode)
            {
                var op = (opcode & 0x3C0u) >> 6;
                return this.Instructions[op].Execute(executionCore, opcode);
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
                new AddInstruction(),
                new AddInstruction(),
                new SubInstruction(),
                new SubInstruction(),
                new AddInstruction(),
                new AddInstruction(),
                new SubInstruction(),
                new SubInstruction(),
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
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 0011
            new ThumbInstruction[]
            {
                // 0---
                new AddInstruction(),
                new AddInstruction(),
                new AddInstruction(),
                new AddInstruction(),
                new AddInstruction(),
                new AddInstruction(),
                new AddInstruction(),
                new AddInstruction(),
                // 1---
                new SubInstruction(),
                new SubInstruction(),
                new SubInstruction(),
                new SubInstruction(),
                new SubInstruction(),
                new SubInstruction(),
                new SubInstruction(),
                new SubInstruction(),
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
                new NopInstruction(),
                new NopInstruction(),
                new MoveInstruction(),
                new BxInstruction(),
                // 1---
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
            },
            // 0101
            new ThumbInstruction[]
            {
                // 0---
                new StrInstruction(),
                new StrInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 0110
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
            },
            // 0111
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 1000
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 1001
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 1010
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 1011
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 1100
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 1101
            new ThumbInstruction[]
            {
                // 0---
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                // 1---
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
                new BranchInstruction(),
            },
            // 1110
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 1111
            new ThumbInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
        };
    }
}
