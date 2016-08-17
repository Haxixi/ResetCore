using UnityEngine;
using System.Collections;

namespace ResetCore.Util
{
    public static class GameObjectEx
    {

        /// <summary>
        /// 重置物体的Transform
        /// </summary>
        /// <param name="go"></param>
        public static void ResetTransform(this GameObject go)
        {
            go.transform.position = Vector3.zero;
            go.transform.eulerAngles = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
        }

        /// <summary>
        /// 虫植物体的Transfrom
        /// </summary>
        /// <param name="tran"></param>
        public static void ResetTransform(this Transform tran)
        {
            tran.position = Vector3.zero;
            tran.eulerAngles = Vector3.zero;
            tran.localScale = Vector3.one;
            tran.gameObject.SetActive(true);
        }

        public static Vector3 NewRotateAround(this Transform tran, Vector3 pos, Vector3 euler)
        {
            Quaternion rotation = Quaternion.Euler(euler) * tran.localRotation;
            Vector3 newPosition = rotation * (tran.position - pos);
            return newPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="go"></param>
        public static GameObjectCallBack GetCallbacks(this GameObject go)
        {
            return go.GetOrCreateComponent<GameObjectCallBack>();
        }

    }

}
