using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Branch;
using NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Arm.Arithmetic;

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
                // 1---
            },
            // 0001
            new ArmInstruction[]
            {
                // 0---
                null,
                null,
                null,
                null,
                new CmpInstruction(),
                new CmpInstruction(),
                null,
                null,
                // 1---
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
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 0011
            new ArmInstruction[]
            {
                // 0---
                null,
                null,
                null,
                null,
                new CmpInstruction(),
                new CmpInstruction(),
                null,
                null,
                // 1---
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            },
            // 0100
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 0101
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 0110
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 0111
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 1000
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 1001
            new ArmInstruction[]
            {
                // 0---
                // 1---
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
                // 1---
            },
            // 1100
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 1101
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 1110
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
            // 1111
            new ArmInstruction[]
            {
                // 0---
                // 1---
            },
        };
    }
}
