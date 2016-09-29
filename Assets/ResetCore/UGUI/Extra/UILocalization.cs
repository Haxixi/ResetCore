using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ResetCore.Event;
using ResetCore.Data;
using ResetCore.Util;

namespace ResetCore.UGUI
{
    [AddComponentMenu("UI/Extra/UILocalization")]
    public class UILocalization : MonoBehaviour
    {
        
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
#if DATA_GENER
                if (string.IsNullOrEmpty(value)) return;

                key = value;
                textToShow = LanguageManager.GetWord(key);
                Text txt = GetComponent<Text>();
                Image image = GetComponent<Image>();
                if(txt != null)
                {
                    txt.text = textToShow;
                }
                if (image != null)
                {
                   //命名要求为： 包名-sprite名
                    if (string.IsNullOrEmpty(textToShow)) return;
                   image.sprite = SpriteHelper.GetSpriteByFullName(textToShow);
                }
#else
                key = value;
#endif
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
}
