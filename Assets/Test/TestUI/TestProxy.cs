using ResetCore.Event;
using ResetCore.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProxy {

    public EventProperty<float> money { get; set; }

    public TestProxy()
    {
        money = new EventProperty<float>();
    }

}
