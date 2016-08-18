using UnityEngine;
using System.Collections;
using ResetCore.Util;
using ResetCore.Asset;
using System.Collections.Generic;

namespace ResetCore.Util
{
    public class EffectPool : MonoSingleton<EffectPool>
    {

        private List<GameObject> EfPool;

        public override void Init()
        {
            base.Init();
            EfPool = new List<GameObject>();
        }

        /// <summary>
        /// 在世界坐标系下播放特效
        /// </summary>
        /// <param name="efName">特效名</param>
        /// <param name="pos">绝对位置</param>
        /// <param name="time">显示时间</param>
        /// <returns></returns>
        public GameObject PlayEffectInRoot(string efName, Vector3 pos, float time = -1, params object[] args)
        {
            GameObject efGo = FindOrCreateObject(efName);
            efGo.transform.position = pos;
            efGo.SetActive(true);
            Play(efGo, args);
            if (time > 0)
            {
                CoroutineTaskManager.Instance.WaitSecondTodo(() =>
                {
                    HideEffect(efGo);
                }, time);
            }

            return efGo;
        }

        /// <summary>
        /// 在对象相对位置下播放特效
        /// </summary>
        /// <param name="efName">特效名</param>
        /// <param name="tran">绑定对象</param>
        /// <param name="localPos">相对位置</param>
        /// <param name="time">显示时间</param>
        /// <returns></returns>
        public GameObject PlayEffectUnderTran(string efName, Transform tran, Vector3 localPos, float time = -1, params object[] args)
        {
            GameObject efGo = FindOrCreateObject(efName);
            efGo.transform.parent = tran;
            efGo.transform.localPosition = localPos;
            efGo.SetActive(true);
            if (time > 0)
            {
                CoroutineTaskManager.Instance.WaitSecondTodo(() =>
                {
                    HideEffect(efGo);
                }, time);
            }
            Play(efGo, args);
            return efGo;
        }

        /// <summary>
        /// 隐藏特效
        /// </summary>
        /// <param name="go"></param>
        public void HideEffect(GameObject go)
        {
            if (go == null) return;
            if (!EfPool.Contains(go))
            {
                Debug.logger.LogWarning("隐藏特效", "特效" + go.name + "不属于特效池");
                return;
            }
            go.SetActive(false);
            go.transform.SetParent(transform);
        }

        private GameObject FindOrCreateObject(string efName)
        {
            GameObject efGo = null;
            efGo = CheckPool(efName);
            if (efGo == null)
            {
                efGo = CreateObject(efName);
                EfPool.Add(efGo);
            }
            return efGo;
        }

        private GameObject CheckPool(string efName)
        {
            for (int i = 0; i < EfPool.Count; i++)
            {
                GameObject go = EfPool[i];
                if (go == null)
                {
                    EfPool.Remove(go);
                    continue;
                }
                string goName = go.name;
                if (goName.Contains("(Clone)"))
                {
                    goName = goName.ReplaceFirst("(Clone)", "");
                }

                if (goName == efName && go.activeSelf == false)
                {
                    return go;
                }
            }
            return null;
        }

        private GameObject CreateObject(string efName)
        {
            GameObject newGameObject = ResourcesLoaderHelper.Instance.LoadAndGetInstance(efName + ".prefab");
            return newGameObject;
        }

        private void Play(GameObject ef, params object[] args)
        {
            BaseEffect baseEffect = ef.GetComponent<BaseEffect>();
            if (baseEffect == null) return;

            baseEffect.Init(args);
            baseEffect.Play(args);
        }
    }
}

