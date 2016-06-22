using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BgApiDriver;

namespace BgApiDriver.BleWrapper {
    public class BleGroup {
        public BleUUID UUID; // UUID of entry, not the value!
        public int HandleStart;
        public int HandleEnd;
        public BleGroup() {
        }
        public BleGroup(BgApi.ble_msg_attclient_group_found_evt_t evt) {
            UUID = new BleUUID(evt.uuid);
            HandleStart = evt.start;
            HandleEnd = evt.end;
        }
        public override string ToString() {
            return "" + UUID.ToShortString() + " (" + HandleStart.ToString() +
                " to " + HandleEnd.ToString() + ")";
        }
    }
}
