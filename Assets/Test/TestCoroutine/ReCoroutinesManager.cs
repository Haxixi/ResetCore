using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResetCore.Util
{
    

    public class ReCoroutinesManager : MonoSingleton<ReCoroutinesManager>{


        public override void Init()
        {
            base.Init();
        }

        private List<ReCoroutine> updateIEnumeratorList = new List<ReCoroutine>();
        private List<ReCoroutine> lateUpdateIEnumeratorList = new List<ReCoroutine>();
        private List<ReCoroutine> fixedUpdateIEnumeratorList = new List<ReCoroutine>();

        private List<ReCoroutine> removeIEnumerator = new List<ReCoroutine>();

        private static float updateDeltaTime = Time.deltaTime;
        private static float lateUpdateDeltaTime = Time.deltaTime;
        private static float fixedUpdateDeltaTime = Time.fixedDeltaTime;

        /// <summary>
        /// 替代用的Coroutine
        /// </summary>
        public static ReCoroutine replaceCoroutine { get; set; }


        /// <summary>
        /// 添加新协程
        /// </summary>
        /// <param name="e"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ReCoroutine AddCoroutine(IEnumerator<float> e, CoroutineType type = CoroutineType.Update)
        {
            return Instance._AddCoroutine(e, type);
        }

        private ReCoroutine _AddCoroutine(IEnumerator<float> e, CoroutineType type = CoroutineType.Update)
        {
            var cor = new ReCoroutine(e, type);

            if (type == CoroutineType.Update)
                updateIEnumeratorList.Add(cor);
            else if (type == CoroutineType.LateUpdate)
                lateUpdateIEnumeratorList.Add(cor);
            else if (type == CoroutineType.FixedUpdate)
                fixedUpdateIEnumeratorList.Add(cor);

            return cor;
        }

        

        // Update is called once per frame
        void Update()
        {
            removeIEnumerator.Clear();
            
            for (int i = 0; i < updateIEnumeratorList.Count; i ++)
            {
                var cor = updateIEnumeratorList[i];

                cor.Update();

                if (cor.isDone)
                {
                    removeIEnumerator.Add(cor);
                    continue;
                }

            }

            for (int i = 0; i < removeIEnumerator.Count; i++)
            {
                updateIEnumeratorList.Remove(removeIEnumerator[i]);
            }
        }

        private void LateUpdate()
        {
            removeIEnumerator.Clear();
            for (int i = 0; i < lateUpdateIEnumeratorList.Count; i++)
            {
                var cor = lateUpdateIEnumeratorList[i];
                cor.LateUpdate();

                if (cor.isDone)
                {
                    removeIEnumerator.Add(cor);
                    continue;
                }
            }

            for (int i = 0; i < removeIEnumerator.Count; i++)
            {
                lateUpdateIEnumeratorList.Remove(removeIEnumerator[i]);
            }
        }

        private void FixedUpdate()
        {
            removeIEnumerator.Clear();
            for (int i = 0; i < fixedUpdateIEnumeratorList.Count; i++)
            {
                var cor = fixedUpdateIEnumeratorList[i];

                cor.FixedUpdate();

                if (cor.isDone)
                {
                    removeIEnumerator.Add(cor);
                    continue;
                }

            }

            for (int i = 0; i < removeIEnumerator.Count; i++)
            {
                fixedUpdateIEnumeratorList.Remove(removeIEnumerator[i]);
            }
        }

        public static float GetDeltaTime(ReCoroutine coroutine)
        {
            switch (coroutine.coroutineType)
            {
                case CoroutineType.Update:
                    return updateDeltaTime;
                case CoroutineType.LateUpdate:
                    return lateUpdateDeltaTime;
                case CoroutineType.FixedUpdate:
                    return fixedUpdateDeltaTime;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 等待www返回
        /// </summary>
        /// <param name="www"></param>
        /// <returns></returns>
        public static float WaitWWW(WWW www)
        {
            replaceCoroutine = AddCoroutine(GetReplaceCoroutine(() => www.isDone), CoroutineType.Update);
            return float.NaN;
        }

        public static IEnumerator<float> GetReplaceCoroutine(Func<bool> func)
        {
            while (!func())
            {
                yield return 0;
            }
        }

    }

}
