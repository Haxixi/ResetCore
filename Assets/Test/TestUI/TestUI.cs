using UnityEngine;
using System.Collections;
using ResetCore.UGUI;
using UnityEngine.UI;
using ResetCore.Event;
using ResetCore.UGUI.View;
using ResetCore.IOC;
using ResetCore.UGUI.Model;
using System.Xml.Linq;
using ResetCore.UGUI.Binder;

public class TestUI : BaseNormalUI {

    [Inject]
    TestUIView v;

    [Inject]
    TestUIModel m;

    [Inject]
    TestUIBinder b;

    public override Context context
    {
        get
        {
            return Contexts.testContext;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        v.Init(this);
        b.Bind(v, m);
    }

    public void OnTestButton()
    {
        v.txtText.text = v.goTestButton.name;
    }

}
