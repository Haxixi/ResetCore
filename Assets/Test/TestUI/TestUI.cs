using UnityEngine;
using System.Collections;
using ResetCore.UGUI;
using UnityEngine.UI;
using ResetCore.Event;
using ResetCore.UGUI.View;
using ResetCore.IOC;
using ResetCore.UGUI.Model;
using System.Xml.Linq;

public class TestUI : BaseNormalUI {

    [Inject]
    TestUIView v;

    [Inject]
    TestUIModel m;

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

        v.txtResult.Bind(m.money);
        v.inputInputField.Bind(m.money)[0].PoolInOneFrame();

    }

    public void OnTestButton()
    {
        v.txtText.text = v.goTestButton.name;
    }

}
