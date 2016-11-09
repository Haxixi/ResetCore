using UnityEngine;
using System.Collections;

public class DeviceManager {

    private Device _currentDevice = null;

    public Device currentDevice
    {
        get
        {
            if(_currentDevice == null)
            {
                _currentDevice = Device.GetDevice();
            }
            return _currentDevice;
        }
    }
}
