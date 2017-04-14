using UnityEngine;
using System.Collections;
using ResetCore.UGUI;
using UnityEngine.UI;
using ResetCore.Event;
using ResetCore.UGUI.View;
using ResetCore.Rx;

public class TestUI : BaseNormalUI {

    TestUIView v = new TestUIView();

    protected override void Awake()
    {
        base.Awake();
        v.Init(this);

        v.inputInputField.onValueChanged.GetListenable().Listen((str) =>
        {
            v.txtText.text = str;
        }).TakeUntil(1);
    }

   public void OnTestButton()
    {
        v.txtText.text = v.goTestButton.name;
        
    }

}
