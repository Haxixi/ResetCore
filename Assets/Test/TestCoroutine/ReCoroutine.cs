using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ResetCore.Util
{
    public enum CoroutineType
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    public class ReCoroutine
    {
        /// <summary>
        /// 协程标识符Id
        /// </summary>
        public long id { get; private set; }

        /// <summary>
        /// 迭代类型
        /// </summary>
        public CoroutineType coroutineType { get; private set; }

        /// <summary>
        /// 迭代器
        /// </summary>
        public IEnumerator<float> e { get; private set; }

        /// <summary>
        /// 距离协程开始的当前时间
        /// </summary>
        public float currentTime { get; private set; }

        /// <summary>
        /// 等待到该时间并继续执行
        /// </summary>
        public float untilTime { get; private set; }

        /// <summary>
        /// 是否已经完成
        /// </summary>
        public bool isDone { get; private set; }

        /// <summary>
        /// 正在等待的别的迭代器
        /// </summary>
        private ReCoroutine waitingCoroutine { get; set; }

        /// <summary>
        /// 是否正在等待
        /// </summary>
        /// <returns></returns>
        public bool IsWaiting()
        {
            if (untilTime > currentTime)
                return true;

            if (waitingCoroutine != null)
                return true;

            return false;
        }

        private static long currentId = 0;
        public ReCoroutine(IEnumerator<float> e, CoroutineType type)
        {
            this.e = e;
            this.coroutineType = type;
            this.currentTime = 0;
            this.untilTime = 0;
            this.isDone = false;
            this.id = currentId++;
        }

        /// <summary>
        /// 等待多少时间
        /// </summary>
        /// <param name="waitTime"></param>
        public void Wait(float waitTime)
        {
            if (waitTime == float.NaN) waitTime = 0;
            untilTime = currentTime + waitTime;
        }

        public void Update()
        {
            if (coroutineType == CoroutineType.Update)
            {
                CommonUpdate();
            }
        }

        public void LateUpdate()
        {
            if (coroutineType == CoroutineType.LateUpdate)
            {
                CommonUpdate();
            }
                
        }

        public void FixedUpdate()
        {
            if (coroutineType == CoroutineType.FixedUpdate)
            {
                CommonUpdate();
            }
                
        }

        private void CommonUpdate()
        {
            currentTime += ReCoroutinesManager.GetDeltaTime(this);
            
            if (!IsWaiting())
            {
                if (!e.MoveNext())
                {
                    isDone = true;
                }
                Wait(e.Current);

                if(e.Current.Equals(float.NaN))
                {
                    waitingCoroutine = ReCoroutinesManager.replaceCoroutine;
                }
            }
            else
            {
                if (waitingCoroutine.isDone)
                {
                    waitingCoroutine = null;
                }
            }


        }

    }

}
