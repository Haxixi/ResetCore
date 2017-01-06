using UnityEngine;
using System.Collections;
using ResetCore.UGUI;
using UnityEngine.UI;
using ResetCore.Event;
using ResetCore.UGUI.Class;

public class TestUI : BaseNormalUI {

    TestUIView v = new TestUIView();

    protected override void Awake()
    {
        base.Awake();
        Debug.logger.Log("asd");
        v.Init(this);
    }

   public void OnTestButton()
    {
        v.txtText.text = v.goTestButton.name;
    }

}
