/*
 * Copyright (c) 2012-2015 Alexander Houben (ahouben@greenliff.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System;

namespace BgApiDriver
{
    public class bd_addr
    {
        public byte[] Address { get; set; }
        public int Length { get { return 6; } }

        public bd_addr()
        {
            Address = new byte[Length];
        }
        public override bool Equals(object obj) {
            bd_addr other = obj as bd_addr;
            if (other == null)
                return false;
            for (int i = 0; i < 6; i++)
                if (other.Address[i] != Address[i])
                    return false;
            return true;
        }
        public override int GetHashCode() {
            int x = BitConverter.ToInt32(Address, 0);
            x = x ^ BitConverter.ToInt16(Address, 4);
            return x;
        }
        public static bool TryParse(string s, out bd_addr result) {
            result = null;
            if (s == null)
                return false;
            s = s.Trim();
            string[] parts = s.Split(new char[] { ':' });
            if (parts.Length != 6)
                return false;
            byte[] parsedBytes = new byte[6];
            try {
                for (int i = 0; i < 6; i++) {
                    if (parts[i].Length < 1 || parts[i].Length > 2)
                        return false;
                    parsedBytes[5-i] = Convert.ToByte(parts[i], 16);
                }
            } catch (Exception) {
                return false;
            }
            result = new bd_addr();
            result.Address = parsedBytes;
            return true;
        }
        public override string ToString()
        {
            string[] res = new string[Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[res.Length-1-i] = Address[i].ToString("X2");
            }
            return string.Join(":", res);
        } 
    }
}
