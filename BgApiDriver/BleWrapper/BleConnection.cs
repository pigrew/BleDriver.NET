using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BgApiDriver.BleWrapper {
    public class BleConnection {
        public int ConnectionHandle { get; set; }
        public Dictionary<BleUUID, BleService> Services;
        bd_addr _Address;
        public bd_addr Address { get { return _Address; } }
        public BleConnection(int connectionHandle, bd_addr address) {
            this.ConnectionHandle = connectionHandle;
            Services = new Dictionary<BleUUID, BleService>();
            this._Address = address;
        }
        public BleService FindServiceByHandle(int handle) {
            foreach(var s in Services) {
                if (handle >= s.Value.HandleStart && handle <= s.Value.HandleEnd)
                    return s.Value;
            }
            return null;
        }
    }
}
