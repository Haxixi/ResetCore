using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ResetCore.Util;
using NetPostUtil;

namespace ResetCore.NetPost
{
    public class NetBehavior : MonoBehaviour
    {
        public int instanceId { get; private set; }

        public virtual void Awake()
        {
            instanceId = gameObject.GetInstanceID();
        }

        public virtual void Start()
        {

        }

        public virtual void OnEnable()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnDestroy()
        {

        }

        
    }

}
