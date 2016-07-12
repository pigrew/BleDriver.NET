using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BgApiDriver.BleWrapper {
    public class BleConnection {
        public int ConnectionHandle { get; set; }
        public Dictionary<BleUUID, BleService> Services;
        public BleConnection(int connectionHandle) {
            this.ConnectionHandle = connectionHandle;
            Services = new Dictionary<BleUUID, BleService>();
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
