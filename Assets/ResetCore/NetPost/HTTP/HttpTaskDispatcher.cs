using UnityEngine;
using System.Collections;
using ResetCore.Util;
using System.Collections.Generic;
using System;
using LitJson;

namespace ResetCore.NetPost
{
    /// <summary>
    /// 网络任务分发器，负责分发网络任务
    /// </summary>
    public class HttpTaskDispatcher
    {

        private static Dictionary<string, ActionQueue> taskTable = new Dictionary<string, ActionQueue>();


        /// <summary>
        /// 添加自定义任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="queueName"></param>
        public static void AddNetPostTask(HttpPostTask task, string queueName = "Defualt")
        {
            Action<Action> postAct = (act) =>
            {
                task.Start(act);
            };
            GetQueue(queueName).AddAction(postAct);
        }

        /// <summary>
        /// 添加通用任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskParams"></param>
        /// <param name="finishCall"></param>
        /// <param name="progressCall"></param>
        /// <param name="queueName"></param>
        public static void AddNetPostTask(int taskId, Dictionary<string, object> taskParams
            , Action<JsonData> finishCall = null, Action<float> progressCall = null, string queueName = "Defualt")
        {
            CommonNetTask task = new CommonNetTask(taskId, taskParams, finishCall, progressCall);
            AddNetPostTask(task, queueName);
        }

        private static ActionQueue GetQueue(string queueName)
        {
            if (!taskTable.ContainsKey(queueName))
            {
                taskTable.Add(queueName, new ActionQueue());
            }
            return taskTable[queueName];
        }
    }

}
