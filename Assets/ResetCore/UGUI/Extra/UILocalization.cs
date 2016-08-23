using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ResetCore.Event;
using ResetCore.Data;
using ResetCore.Util;

[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("UI/Extra/UILocalization")]
public class UILocalization : MonoBehaviour {

    private bool started = false;

    public string key;
    private string textToShow;
    public string val
    {
        get
        {
            return textToShow;
        }
        set
        {
            if (string.IsNullOrEmpty(value)) return;

            key = value;
            textToShow = LanguageManager.GetWord(key);
            Text txt = GetComponent<Text>();

            if(txt != null)
            {
                txt.text = textToShow;
            }
        }
    } 

    private void OnLocalize()
    {
        val = key;
    }

    void Awake()
    {
        EventDispatcher.AddEventListener(InnerEvents.UGUIEvents.OnLocalize, OnLocalize);
    }

    void OnDestroy()
    {
        EventDispatcher.RemoveEventListener(InnerEvents.UGUIEvents.OnLocalize, OnLocalize);
    }

    void Start()
    {
        started = true;
        OnLocalize();
    }

    void OnEnable()
    {
        if (started)
        {
            OnLocalize();
        }
    }

}
