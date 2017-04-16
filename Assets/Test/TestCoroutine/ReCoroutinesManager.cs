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

        List<ReCoroutine> updateIEnumeratorList = new List<ReCoroutine>();
        List<ReCoroutine> lateUpdateIEnumeratorList = new List<ReCoroutine>();
        List<ReCoroutine> fixedUpdateIEnumeratorList = new List<ReCoroutine>();

        public void AddCoroutine(IEnumerator e, CoroutineType type = CoroutineType.Update)
        {
            updateIEnumeratorList.Add(new ReCoroutine(e, type));
        }

        List<ReCoroutine> removeIEnumerator = new List<ReCoroutine>();
        List<float> currentUpdateEnumeratorTime = new List<float>();

        // Update is called once per frame
        void Update()
        {
            removeIEnumerator.Clear();
            foreach (var cor in updateIEnumeratorList)
            {
                cor.Update();

                if (cor.IsWaiting())
                    continue;

                if (cor.e.Current is float || cor.e.Current is int)
                {
                    cor.Wait((float)cor.e.Current);
                }

                if (cor.e.Current is ReCoroutine)
                {
                    cor.WaitOtherCoroutine((ReCoroutine)cor.e.Current);
                }

                if (!cor.e.MoveNext())
                {
                    removeIEnumerator.Add(cor);
                    continue;
                }

            }

            for(int i = 0; i<removeIEnumerator.Count; i++)
            {
                updateIEnumeratorList.Remove(removeIEnumerator[i]);
            }
        }

        private void LateUpdate()
        {
            removeIEnumerator.Clear();
            foreach (var cor in lateUpdateIEnumeratorList)
            {
                cor.LateUpdate();

                if (cor.IsWaiting())
                    continue;

                if (cor.e.Current is float)
                {
                    cor.Wait(3);
                }

                if(cor.e.Current is ReCoroutine)
                {
                    cor.WaitOtherCoroutine((ReCoroutine)cor.e.Current);
                }

                if (!cor.e.MoveNext())
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

        private void FixedUpdate()
        {
            removeIEnumerator.Clear();
            foreach (var cor in fixedUpdateIEnumeratorList)
            {
                cor.FixedUpdate();

                if (cor.IsWaiting())
                    continue;

                if (cor.e.Current is float)
                {
                    cor.Wait(3);
                }

                if (cor.e.Current is ReCoroutine)
                {
                    cor.WaitOtherCoroutine((ReCoroutine)cor.e.Current);
                }

                if (!cor.e.MoveNext())
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

        //public static IEnumerator TestWaitTime(float seconds)
        //{
        //    float start = 0;
        //    while (start < seconds)
        //    {
        //        start += Time.deltaTime;
        //        yield return null;
        //    }
        //}

        public static float GetDeltaTime(ReCoroutine coroutine)
        {
            switch (coroutine.coroutineType)
            {
                case CoroutineType.Update:
                    return Time.deltaTime;
                case CoroutineType.LateUpdate:
                    return Time.deltaTime;
                case CoroutineType.FixedUpdate:
                    return Time.fixedDeltaTime;
                default:
                    return 0;
            }
        }
    }

}
