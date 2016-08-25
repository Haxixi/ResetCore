using UnityEngine;
using System.Collections;
using ResetCore.Util;
using ResetCore.Asset;

namespace ResetCore.Util
{
    public class GameInitManager : MonoBehaviour
    {
        static System.Type[] initCompTypes = new System.Type[]{
            //typeof(GameInitManager), typeof(CoroutineTaskManager), typeof(ResourcesLoaderHelper), typeof(ModManager)
        };

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
        }
    }

}
