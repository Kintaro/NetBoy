using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netboy.Core.Rom
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Header
    {
        public uint RomEntryPoint;
        public byte[] NintendoLogo;
        public byte[] GameTitle;
        public byte[] GameCode;
        public byte[] MakerCode;
        public byte FixedValue;
        public byte MainUnitCode;
        public byte DeviceType;
        public byte[] ReservedArea = new byte[7];
        public byte SoftwareVersion;
        public byte ComplementCheck;
        public byte[] ReservedArea2 = new byte[2];
        public uint RamEntryPoint;
        public byte BootMode;
        public byte SlaveIdNumber;
        public byte[] Unused = new byte[26];
        public uint JoybusEntryPoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void ReadHeaderFromStream(BinaryReader reader)
        {
            this.RomEntryPoint = reader.ReadUInt32();
            this.NintendoLogo = reader.ReadBytes(156);
            this.GameTitle = reader.ReadBytes(12);
            this.GameCode = reader.ReadBytes(4);
            this.MakerCode = reader.ReadBytes(2);
            this.FixedValue = reader.ReadByte();
            this.MainUnitCode = reader.ReadByte();
            this.DeviceType = reader.ReadByte();
            this.ReservedArea = reader.ReadBytes(7);
            this.SoftwareVersion = reader.ReadByte();
            this.ComplementCheck = reader.ReadByte();
            this.ReservedArea2 = reader.ReadBytes(2);
            this.RamEntryPoint = reader.ReadUInt32();
            this.BootMode = reader.ReadByte();
            this.SlaveIdNumber = reader.ReadByte();
            this.Unused = reader.ReadBytes(26);
            this.JoybusEntryPoint = reader.ReadUInt32();
        }
    }
}
