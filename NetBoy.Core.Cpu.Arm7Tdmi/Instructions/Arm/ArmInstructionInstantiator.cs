using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Branch;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Arithmetic;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Logical;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Memory;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ArmInstructionInstantiator
    {
        public ArmInstruction[][] Instructions = new ArmInstruction[][]
        {
            // 0000
            new ArmInstruction[]
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
            // 0001
            new ArmInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new BxInstruction(),
                new TeqInstruction(),
                new CmpInstruction(),
                new CmpInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 0010
            new ArmInstruction[]
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
            // 0011
            new ArmInstruction[]
            {
                // 0---
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new TeqInstruction(),
                new CmpInstruction(),
                new CmpInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                // 1---
                new NopInstruction(),
                new NopInstruction(),
                new MoveInstruction(),
                new MoveInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
                new NopInstruction(),
            },
            // 0100
            new ArmInstruction[]
            {
                // 0---
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
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
            new ArmInstruction[]
            {
                // 0---
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
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
            // 0110
            new ArmInstruction[]
            {
                // 0---
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
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
            new ArmInstruction[]
            {
                // 0---
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
                new LdrInstruction(),
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
            // 1000
            new ArmInstruction[]
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
            new ArmInstruction[]
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
            new ArmInstruction[]
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
            // 1011
            new ArmInstruction[]
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
            new ArmInstruction[]
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
            new ArmInstruction[]
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
            // 1110
            new ArmInstruction[]
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
            new ArmInstruction[]
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
