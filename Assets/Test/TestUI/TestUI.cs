using UnityEngine;
using System.Collections;
using ResetCore.UGUI;
using UnityEngine.UI;
using ResetCore.Event;
using ResetCore.UGUI.View;
using ResetCore.IOC;
using System;

public class TestUI : BaseNormalUI {

    [Inject]
    TestUIView v;

    [Inject]
    TestProxy p;

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

        v.txtResult.Bind(p.money);
        v.inputInputField.Bind(p.money, (input, str)=> input.text = str.propValue.ToString("F0"));

    }

    public void OnTestButton()
    {
        v.txtText.text = v.goTestButton.name;
    }

}
