﻿using UnityEngine;
using System.Collections;

namespace ResetCore.UGUI
{
    public abstract class ShowUIArg{}

    public abstract class BaseUI : MonoBehaviour
    {

        public Transform uiRoot { get; protected set; }

        [SerializeField]
        private UIConst.UIName _uiName;
        public UIConst.UIName uiName
        {
            get { return _uiName; }
            set { _uiName = value; }
        }

        protected virtual void Awake() { }

        protected virtual void OnEnable() { }

        protected virtual void Start() { }

        public virtual void Init(ShowUIArg arg) 
        {
            if (arg == null) return;
        }
        protected virtual void Update() { }

        protected virtual void OnDisable() { }

        public virtual void Hide(System.Action afterAct = null)
        {
            gameObject.SetActive(false);
        }
    }
}

