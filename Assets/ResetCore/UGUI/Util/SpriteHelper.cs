using UnityEngine;
using System.Collections;
using ResetCore.Asset;
using ResetCore.UGUI;
using ResetCore.Util;

namespace ResetCore.UGUI
{
    public class SpriteHelper
    {

        //public static Sprite GetSprite(string spriteName, Rect rect = default(Rect), Vector2 pivot = default(Vector2))
        //{
        //    Texture2D texture = ResourcesLoaderHelper.Instance.LoadResource<Texture2D>(spriteName);

        //    Rect finRect = rect;
        //    Vector2 finPivot = pivot;

        //    if (rect == default(Rect))
        //    {
        //        finRect = new Rect(0, 0, texture.width, texture.height);
        //    }
        //    if (pivot == default(Vector2))
        //    {
        //        finPivot = new Vector2(0.5f, 0.5f);
        //    }

        //    Sprite sprite = Sprite.Create(texture, finRect, finPivot);
        //    return sprite;
        //}

        public static Sprite GetSprite(string spriteName, string packageName = UIConst.defaultPackage)
        {
#if ASSET && !UNITY_EDITOR
            GameObject go = ResourcesLoaderHelper.Instance.LoadResource(packageName + "-" + spriteName) as GameObject;
            if(go == null)
            {
                return null;
            }
            return go.GetComponent<SpriteRenderer>().sprite;
#else
            GameObject go = Resources.Load<GameObject>(PathEx.Combine(UIConst.spritePrefabPath, packageName, packageName + "-" + spriteName));
            if (go == null)
            {
                return null;
            }
            return go.GetComponent<SpriteRenderer>().sprite;
#endif
        }

        public static Sprite GetSpriteByFullName(string fullSpriteName)
        {
#if ASSET && !UNITY_EDITOR
            GameObject go = ResourcesLoaderHelper.Instance.LoadResource(fullSpriteName) as GameObject;
            if(go == null)
            {
                return null;
            }
            return go.GetComponent<SpriteRenderer>().sprite;
#else
            GameObject go = Resources.Load<GameObject>(PathEx.Combine(UIConst.spritePrefabPath, fullSpriteName.Split('-')[0], fullSpriteName));
            if(go == null)
            {
                return null;
            }
            return go.GetComponent<SpriteRenderer>().sprite;
#endif
        }
    }
}
