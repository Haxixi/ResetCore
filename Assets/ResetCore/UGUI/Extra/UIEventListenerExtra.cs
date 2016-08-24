using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if EVENT
using ResetCore.Event;
#endif
#if OBJECT
using ResetCore.ResObject;
#endif

namespace ResetCore.UGUI
{
    [AddComponentMenu("UI/Extra/UIEventListenerExtra")]
    [RequireComponent(typeof(UIEventListener))]
    public class UIEventListenerExtra : MonoBehaviour
    {
        private UIEventListener _lis;
        private UIEventListener lis
        {
            get
            {
                if(_lis == null)
                {
                    _lis = gameObject.GetComponent<UIEventListener>();
                }
                return _lis;
            }
        }

        void Awake()
        {
#if EVENT
            AddEvent();
#endif
#if OBJECT
            AddAudioClip();
#endif
        }

#if EVENT
#region Event
        [Header("Trigger Event Names")]
        [Space]
        public List<string> pointerClickEventList;
        public List<string> pointDownEventList;
        public List<string> pointEnterEventList;
        public List<string> pointExitEventList;
        public List<string> pointUpEventList;
        public List<string> selectEventList;
        public List<string> updateSelectedEventList;

        private void AddEvent()
        {
            AddEventToDeleget(pointerClickEventList, lis.onClick);
            AddEventToDeleget(pointDownEventList, lis.onDown);
            AddEventToDeleget(pointEnterEventList, lis.onEnter);
            AddEventToDeleget(pointExitEventList, lis.onExit);
            AddEventToDeleget(pointUpEventList, lis.onUp);
            AddEventToDeleget(selectEventList, lis.onSelect);
            AddEventToDeleget(updateSelectedEventList, lis.onUpdateSelect);

        }

        private void AddEventToDeleget(List<string> eventNameList, UIEventListener.VoidDelegate on)
        {
            if (eventNameList != null && on != null)
            {
                on += (go) =>
                {
                    eventNameList.ForEach((eventName) =>
                    {
                        EventDispatcher.TriggerEvent(eventName);
                    });
                };
            }
        }
#endregion
#endif

#if OBJECT
        [Header("Trigger Event Audio Clips")]
        [Space]
        public string pointerClickEventClip;
        public string pointDownEventClip;
        public string pointEnterEventClip;
        public string pointExitEventClip;
        public string pointUpEventClip;
        public string selectEventClip;
        public string updateSelectedEventClip;

        private void AddAudioClip()
        {
            AddAudioClipToDelegate(pointerClickEventClip, lis.onClick);
            AddAudioClipToDelegate(pointDownEventClip, lis.onDown);
            AddAudioClipToDelegate(pointEnterEventClip, lis.onEnter);
            AddAudioClipToDelegate(pointExitEventClip, lis.onExit);
            AddAudioClipToDelegate(pointUpEventClip, lis.onUp);
            AddAudioClipToDelegate(selectEventClip, lis.onSelect);
            AddAudioClipToDelegate(updateSelectedEventClip, lis.onUpdateSelect);
        }

        private void AddAudioClipToDelegate(string clipName, UIEventListener.VoidDelegate on)
        {
            if (string.IsNullOrEmpty(clipName) && on != null)
            {
                on += (go) =>
                {
                    AudioManager.Instance.PlayGlobalSE(clipName);
                };
            }
        }
#endif

    }

}
