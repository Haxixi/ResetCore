using ResetCore.IOC;
using ResetCore.UGUI.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestContext : Context<TestContext> {

	[Bean]
    public TestUIView GetView()
    {
        var v = new TestUIView();
        return v;
    }

    [Bean]
    public TestProxy GetProxy()
    {
        var p = new TestProxy();
        p.money.propValue = 10;
        return p;
    }

}
