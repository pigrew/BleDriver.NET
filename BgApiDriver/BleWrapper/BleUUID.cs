using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BgApiDriver.BleWrapper {
    public class BleUUID {
        public static BleUUID PrimaryService =
            new BleUUID("0x2800");
        public static BleUUID SecondaryService =
            new BleUUID("0x2801");
        public static BleUUID Characteristic =
            new BleUUID("0x2803");
        public static BleUUID ClientCharacteristicConfiguration =
            new BleUUID("0x2902");
        public BleUUID(byte[] data) {
            this.data = data;
        }
        /// <summary>
        // Can parse (MSB first):
        // ABCD 
        // 0xABCD
        // 81a130d2-502f-4cf1-a376-63edeb000e9f
        /// </summary>
        /// <param name="uuidString"></param>
        public BleUUID(string uuidString) {
            uuidString = uuidString.Trim();
            if (uuidString.StartsWith("0x"))
                uuidString = uuidString.Substring(2);
            uuidString = uuidString.Replace("-", "");
            if(uuidString.Length != 4 &&
                uuidString.Length != 8 &&
                uuidString.Length != 32)
                throw new FormatException("Invalid UUID1");
            data = new byte[uuidString.Length / 2];
            for(int i=0; i<data.Length; i++)
                data[data.Length-1-i] = Convert.ToByte(uuidString.Substring(i*2, 2), 16);
        }
        /// <summary>
        /// Internal UUID data.
        /// 
        /// Byte 0 is the LSB, as required by the BgAPI.
        /// </summary>
        public byte[] data { get; set; }
        public int Length { get { return data.Length; } }
        // Attempt to return the short (16- or 32-bit) version of the UUID
        public string ToShortString() {
            if (data.Length == 16 &&
                data[11] == 0x00 && data[10] == 0x00 &&
                data[9] == 0x10 && data[8] == 0x00 &&
                data[7] == 0x80 && data[6] == 0x00 &&
                data[5] == 0x00 && data[4] == 0x80 &&
                data[3] == 0x5F && data[2] == 0x9B &&
                data[1] == 0x34 && data[0] == 0xFB) {
                if (data[15] == 0x00 && data[14] == 0x00)
                    return "0x" + data[13].ToString("X2") + data[12].ToString("X2");
                else
                    return "0x" + data[15].ToString("X2") + data[14].ToString("X2") +
                        data[13].ToString("X2") + data[12].ToString("X2");
            } else
                return ToString();
        }
        public override string ToString() {
            if (Length == 2)
                return "0x" + data[1].ToString("X2") + data[0].ToString("X2");

            string[] res = new string[Length / 2];
            for (int i = 0; i < Length / 2; i++) {
                int word = data[2 * i + 1];
                word = word * 256 + data[2 * i];
                res[i] = (word).ToString("x4");
            }
            if (res.Length == 8)
                return res[7] + res[6] + "-" + res[5] + "-" + res[4] + "-" + res[3] + "_" + res[2] + res[1] + res[0];
            return string.Join("::", res.Reverse());
        }
        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            BleUUID other = obj as BleUUID;
            if (other == null)
                return false;
            return this.Equals(other);
        }
        public bool Equals(BleUUID other) {
            if (other == null)
                return false;
            if ((other.data == null) &&
                (data == null))
                return true;
            if (data == null || other.data == null)
                return false;
            if (data.Length != other.data.Length)
                return false;
            for (int i = 0; i < data.Length; i++)
                if (data[i] != other.data[i])
                    return false;
            return true;
        }
        public int ToInt() {
            if (data.Length == 2)
                return (((int)data[1]) << 8) + data[0];
            throw new Exception();
        }
        public override int GetHashCode() {
            if (data == null || data.Length == 0)
                return -1;
            var hashCode = 0;
            for (var i = 0; i < data.Length; i++)
                // Rotate by 3 bits and XOR the new value.
                hashCode = (hashCode << 3) | (hashCode >> (29)) ^ data[i];
            return hashCode;
        }
    }
}
