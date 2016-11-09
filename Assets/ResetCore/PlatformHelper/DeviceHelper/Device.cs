using UnityEngine;
using System.Collections;

public abstract class Device {

    public static Device GetDevice()
    {
        if (Application.platform == RuntimePlatform.Android)
            return new AndroidDevice();
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            return new IOSDevice();
        else if (Application.platform == RuntimePlatform.WindowsEditor || 
            Application.platform == RuntimePlatform.WindowsPlayer)
            return new PCDevice();
        return null;
    }
}
