using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BgApiDriver.BleWrapper {
    public class BleCharacteristic: BleGroup {
        public List<BleDescriptor> Descriptors;
        public BleDescriptor ValueDescriptor;
        public BleCharacteristic() {
            Descriptors = new List<BleDescriptor>();
        }
    }
}
