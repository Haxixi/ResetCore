using UnityEngine;
using System.Collections;
using ResetCore.Event;
using ResetCore.NetPost;
using Protobuf.Data;

public class TestCube : NetBehavior<Vector3DData> {

    public override void Awake()
    {
        base.Awake();
        EventDispatcher.AddEventListener<Vector3DData>("TestHandler", HandlePosition);
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    public override void Update () {
        Control();

    }

    private void Control()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position = transform.position + new Vector3(-0.1f, 0, 0);

            Vector3 newPos = transform.position + new Vector3(-0.1f, 0, 0);
            Vector3DData vector = new Vector3DData();
            vector.X = newPos.x;
            vector.Y = newPos.y;
            vector.Z = newPos.z;
            NetSceneManager.Instance.currentServer.Send<Vector3DData>
                ((int)HandlerConst.RequestId.TestHandler, 1, vector, SendType.TCP);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position = transform.position + new Vector3(0.1f, 0, 0);

            Vector3 newPos = transform.position + new Vector3(0.1f, 0, 0);
            Vector3DData vector = new Vector3DData();
            vector.X = newPos.x;
            vector.Y = newPos.y;
            vector.Z = newPos.z;
            NetSceneManager.Instance.currentServer.Send<Vector3DData>
                ((int)HandlerConst.RequestId.TestHandler, 1, vector, SendType.TCP);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //transform.position = transform.position + new Vector3(0, 0.1f, 0);

            Vector3 newPos = transform.position + new Vector3(0, 0.1f, 0);
            Vector3DData vector = new Vector3DData();
            vector.X = newPos.x;
            vector.Y = newPos.y;
            vector.Z = newPos.z;
            NetSceneManager.Instance.currentServer.Send<Vector3DData>
                ((int)HandlerConst.RequestId.TestHandler, 1, vector, SendType.TCP);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //transform.position = transform.position + new Vector3(0, -0.1f, 0);

            Vector3 newPos = transform.position + new Vector3(0, -0.1f, 0);
            Vector3DData vector = new Vector3DData();
            vector.X = newPos.x;
            vector.Y = newPos.y;
            vector.Z = newPos.z;
            NetSceneManager.Instance.currentServer.Send<Vector3DData>
                ((int)HandlerConst.RequestId.TestHandler, 1, vector, SendType.TCP);
        }
    }

    public override void OnDestroy()
    {
        EventDispatcher.RemoveEventListener<Vector3DData>("TestHandler", HandlePosition);
    }

    void HandlePosition(Vector3DData vector)
    {
        Vector3 res = new Vector3(vector.X, vector.Y, vector.Z);
        if (transform.position != res)
        {
            transform.position = res;
        }
    }
}
