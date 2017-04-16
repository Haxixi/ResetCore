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
        /// 迭代类型
        /// </summary>
        public CoroutineType coroutineType { get; private set; }

        /// <summary>
        /// 迭代器
        /// </summary>
        public IEnumerator e { get; private set; }

        /// <summary>
        /// 距离协程开始的当前时间
        /// </summary>
        public float currentTime { get; private set; }
        
        /// <summary>
        /// 等待到该时间并继续执行
        /// </summary>
        public float untilTime { get; private set; }

        public ReCoroutine waitCoroutine { get; private set; }

        /// <summary>
        /// 是否正在等待
        /// </summary>
        /// <returns></returns>
        public bool IsWaiting()
        {
            if (untilTime > currentTime)
                return true;

            if (waitCoroutine != null && waitCoroutine.e.MoveNext())
                return true;

            return false;
        }

        public ReCoroutine(IEnumerator e, CoroutineType type)
        {
            this.e = e;
            this.coroutineType = coroutineType;
        }

        /// <summary>
        /// 等待多少时间
        /// </summary>
        /// <param name="waitTime"></param>
        public void Wait(float waitTime)
        {
            untilTime += waitTime;
        }

        public void WaitOtherCoroutine(ReCoroutine cor)
        {

        }

        public void Update()
        {
            if (coroutineType == CoroutineType.Update)
                currentTime += ReCoroutinesManager.GetDeltaTime(this);
        }

        public void LateUpdate()
        {
            if (coroutineType == CoroutineType.LateUpdate)
                currentTime += ReCoroutinesManager.GetDeltaTime(this);
        }

        public void FixedUpdate()
        {
            if (coroutineType == CoroutineType.FixedUpdate)
                currentTime += ReCoroutinesManager.GetDeltaTime(this);
        }

    }

}
