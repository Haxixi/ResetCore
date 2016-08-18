using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ResetCore.Asset;
using ResetCore.Util;

namespace ResetCore.AR
{
    public class ARTrackedData
    {
        public ARTrackedData(string datasetName, ARTrackedObject trackedScene, ARMarker marker)
        {
            this.datasetName = datasetName;
            this.trackedScene = trackedScene;
            this.marker = marker;
        }

        public string datasetName { get; private set; }
        public ARTrackedObject trackedScene { get; private set; }
        public ARMarker marker { get; private set; }
    }

    public class ARManager : MonoSingleton<ARManager>
    {
        public ARController arController;
        public AROrigin arOrigin;
        public ARCamera arCamera;

        private Dictionary<string, ARTrackedData> arTrackedObjectList = new Dictionary<string, ARTrackedData>();

        void Awake()
        {
            CheckAllARTrackedObj();
        }

        /// <summary>
        /// 检查所有的ar追踪物体
        /// </summary>
        private void CheckAllARTrackedObj()
        {
            ARTrackedObject[] trackedObjGroup = arOrigin.GetComponentsInChildren<ARTrackedObject>();
            foreach (ARTrackedObject trackedObj in trackedObjGroup)
            {
                ARMarker marker = trackedObj.GetMarker();
                if (trackedObj.GetMarker() == null)
                {
                    Destroy(trackedObj.gameObject);
                }
                else
                {
                    arTrackedObjectList.Add(marker.NFTDataName, new ARTrackedData(marker.NFTDataName, trackedObj, marker));
                }
            }
        }

        /// <summary>
        /// 动态添加跟踪物体
        /// </summary>
        /// <param name="objName">创建的物体名</param>
        /// <param name="dataName">图像数据名</param>
        /// <returns></returns>
        public GameObject AddNewTrackObject(string objName, string dataName)
        {
            if (!arTrackedObjectList.ContainsKey(dataName))
            {
                CreateNewTrackScene(dataName);
            }
            GameObject obj = ResourcesLoaderHelper.Instance.LoadAndGetInstance(objName);
            obj.transform.parent = arTrackedObjectList[dataName].trackedScene.transform;

            return obj;
        }

        /// <summary>
        /// 获取跟踪物体
        /// </summary>
        /// <param name="dataName">图像数据名</param>
        /// <returns></returns>
        public ARTrackedData GetTrackObject(string dataName)
        {
            ARTrackedData data = null;
            if(!arTrackedObjectList.TryGetValue(dataName, out data))
            {
                Debug.logger.LogError("GetARTrackObject", "Can not find this data in the dictionary");
                return null;
            }
            return data;
        }

        /// <summary>
        /// 移除特定数据对象的追踪场景
        /// </summary>
        /// <param name="dataName">图像数据名</param>
        public void RemoveTrackObject(string dataName)
        {
            ARTrackedData data = null;
            if (!arTrackedObjectList.TryGetValue(dataName, out data))
            {
                Debug.logger.LogError("GetARTrackObject", "Can not find this data in the dictionary");
                return;
            }

            Destroy(data.marker);
            Destroy(data.trackedScene);
        }

        /// <summary>
        /// 创建新的场景
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
        private ARTrackedData CreateNewTrackScene(string dataName)
        {
            string sceneName = dataName + "Scene";
            GameObject newObj = new GameObject(sceneName);
            newObj.transform.parent = arOrigin.transform;

            ARTrackedObject trackedObj = newObj.AddComponent<ARTrackedObject>();
            if (trackedObj == null)
            {
                Destroy(newObj);
                Debug.logger.LogError("Load AR Obj", "Can not find the ARTrackedObject Component");
                return null;
            }
            ARMarker marker = arController.gameObject.AddComponent<ARMarker>();

            trackedObj.secondsToRemainVisible = 1;
            trackedObj.MarkerTag = sceneName;
            marker.Tag = sceneName;

            ARTrackedData data = new ARTrackedData(dataName, trackedObj, marker);
            arTrackedObjectList.Add(dataName, data);

            return data;
        }
    }

}
