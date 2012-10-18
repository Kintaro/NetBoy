using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Instructions.Thumb.Arithmetic
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AddInstruction : ThumbInstruction
    {
        public override bool Execute(ExecutionCore executionCore, uint opcode)
        {
            if ((opcode & 0xF800u) >> 11 == 3)
            {
                var rd = opcode & 0x7u;
                var rs = (opcode & 0x38u) >> 3;
                var rn = (opcode & 0x1C0u) >> 6;

                var op = (opcode & 0x600u) >> 9;

                var rsV = BitConverter.ToInt32(BitConverter.GetBytes((ushort)executionCore.R(rd).Value), 0);
                var rnV = BitConverter.ToInt32(BitConverter.GetBytes((ushort)executionCore.R(rn).Value), 0);
                var r = rsV + rnV;

                if (op == 0)
                    executionCore.R(rd).Value = BitConverter.ToUInt32(BitConverter.GetBytes(r), 0);
                else if (op == 2)
                    executionCore.R(rd).Value = BitConverter.ToUInt32(BitConverter.GetBytes(executionCore.R(rs).Value + rn), 0);
            }
            else if ((opcode & 0xF800u) >> 11 == 6)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = opcode & 0xFFu;

                var rsV = BitConverter.ToInt16(BitConverter.GetBytes((ushort)executionCore.R(rd).Value), 0);
                var rnV = BitConverter.ToInt16(BitConverter.GetBytes((ushort)nn), 0);
                var r = rsV + rnV;

                executionCore.R(rd).Value = BitConverter.ToUInt32(BitConverter.GetBytes(r), 0);
            }
            return false;
        }

        public override string InstructionAsString(uint opcode)
        {
            if ((opcode & 0xF800u) >> 11 == 3)
            {
                var rd = opcode & 0x7u;
                var rs = (opcode & 0x38u) >> 3;
                var rn = (opcode & 0x1C0u) >> 6;

                var op = (opcode & 0x600u) >> 9;

                if (op == 0)
                    return "add #" + rd + ", #" + rs + ", #" + rn;
                else if (op == 2)
                    return "add #" + rd + ", #" + rs + ", " + rn;
            }
            else if ((opcode & 0xF800u) >> 11 == 6)
            {
                var rd = (opcode & 0x700u) >> 8;
                var nn = opcode & 0xFFu;

                return "add #" + rd + ", " + nn;
            }

            throw new NotSupportedException();
        } 
    }
}
