using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;

namespace ResetCore.NetPost
{
    public abstract class HttpPostTask
    {

        public Dictionary<string, object> taskParams { get; private set; }
        public JsonData postJsonData { get; private set; }
        public Action<JsonData> finishCall { get; private set; }
        public Action<float> progressCall { get; private set; }

        private Action afterAct;

        public abstract int taskId
        {
            get;
        }

        public HttpPostTask(Dictionary<string, object> taskParams, Action<JsonData> finishCall = null, Action<float> progressCall = null)
        {
            this.taskParams = taskParams;

            this.finishCall = (backJsonData) =>
            {
                OnFinish(backJsonData);
                if (finishCall != null)
                    finishCall(backJsonData);
                if (afterAct != null)
                    afterAct();
            };
            this.progressCall = (progress) =>
            {
                if (progressCall != null)
                    progressCall(progress);
                OnProgress(progress);
            };

            postJsonData = new JsonData();

            postJsonData["TaskId"] = taskId;

            JsonData subData = new JsonData();
            if(taskParams != null)
            {
                foreach (KeyValuePair<string, object> param in taskParams)
                {
                    subData[param.Key] = new JsonData(param.Value);
                }
            }
            postJsonData["Param"] = subData;
        }

        public void Start(Action afterAct = null, string url = ServerConst.HttpNetPostURL)
        {
            OnStart();
            this.afterAct = afterAct;
            HttpProxy.Instance.AsynDownloadJsonData(url, postJsonData, finishCall, progressCall);
        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnProgress(float progress)
        {
            Debug.logger.Log(progress + "%");
        }

        protected virtual void OnFinish(JsonData backJsonData)
        {
            HandleError(backJsonData);
        }

        private static void HandleError(JsonData backJsonData)
        {
            
            if (backJsonData.ToJson() == "time")
            {
                Debug.LogError("超时");
                return;
            }
            if (backJsonData.ToJson() == "erro")
            {
                Debug.LogError("错误");
                return;
            }
        }

    }

}
