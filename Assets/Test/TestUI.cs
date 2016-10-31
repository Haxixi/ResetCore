using UnityEngine;
using System.Collections;
using ResetCore.UGUI;
using UnityEngine.UI;
using ResetCore.Event;

public class TestUI : BaseNormalUI {

    UIView v;

    protected override void Awake()
    {
        base.Awake();
        v = new UIView(this);

    }

   public void OnTestButton()
    {
        
        v.GetUIByName<Text>("Text").text = "HAHAHA";

        
    }

}
