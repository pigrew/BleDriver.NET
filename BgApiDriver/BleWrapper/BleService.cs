using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BgApiDriver.BleWrapper {
    public class BleService : BleGroup {
        public BleService(BleGroup group) {
            this.UUID = group.UUID;
            this.HandleStart = group.HandleStart;
            this.HandleEnd = group.HandleEnd;
            Characteristics = new Dictionary<BleUUID, BleCharacteristic>();
        }
        public Dictionary<BleUUID, BleCharacteristic> Characteristics;
    }
}
