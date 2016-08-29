using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ResetCore.UGUI
{
    public static class ImageEx
    {
        /// <summary>
        /// 设置Image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="spriteName"></param>
        /// <param name="packageName"></param>
        public static void SetImage(this Image img, string spriteName, string packageName = UIConst.defaultPackage)
        {
            img.sprite = SpriteHelper.GetSprite(spriteName, packageName);
        }
        
    }

}
