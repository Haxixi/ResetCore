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

        v.btnTestButton.onClick.GetListenable().Listen(() =>
        {
            Debug.Log("DoubleClick");
        }).PoolByNum(2).ResetPoolByTime(1);
    }

   public void OnTestButton()
    {
        v.txtText.text = v.goTestButton.name;
        
    }

}
