using UnityEngine;
using System.Collections;


namespace ResetCore.Util
{
    public abstract class BaseEffect : MonoBehaviour
    {
        public abstract void Init(params object[] arg);
        public virtual void Play(params object[] arg) { }
    }

}
