﻿/*
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
    /// <summary>
    /// Base class for all types of packets going over the wire.
    /// </summary>
    public class BgApiPacket
    {
        public byte[] Data { get; set; }

        // header
        public int Length { get { return ((Data[0] & 0x7f) << 8) | Data[1]; } }
        public int Class { get { return Data[2]; } }
        public int Id { get { return Data[3]; } }
    }

    /// <summary>
    /// Base class for events and responses.
    /// </summary>
    public class BgApiEventResponse : EventArgs
    {
        public BgApiEventResponse() { }
        /// <summary>
        /// Construct a new BgApiEventResponse with given packet data
        /// </summary>
        /// <param name="data">Packet data</param>
        public BgApiEventResponse(byte[] data) {
            Packet.Data = data;
        }

        public BgApiPacket Packet = new BgApiPacket();

        public bool IsEvent { get { return (Packet.Data[0] & (byte)ble_msg_types.ble_msg_type_evt) == (byte)ble_msg_types.ble_msg_type_evt; } }

        /// <summary>
        /// The result of a response or event.
        /// </summary>
        public int result = (int)ble_error.ble_err_success;
    }
    public class BgApiEvent : BgApiEventResponse { }
    public class BgApiResponse : BgApiEventResponse { }

    /// <summary>
    /// Base class for commands.
    /// </summary>
    public class BgApiCommand : BgApiPacket { }

}
