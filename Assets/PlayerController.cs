using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float CameraLockTrigger = 0.4f;
    public GameObject Head;
    public float GrowRate;

    float LeftBounds, RightBounds;
    float AxisMin = 0.2f;
    bool isCameraLocked = false;

    // Use this for initialization
    void Start () {

        // Get orthographic bounds of main camera
        Camera camera = Camera.main;
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        LeftBounds = bounds.min.x;
        RightBounds = bounds.max.x;
    }

    void Update() {
        if (!isCameraLocked) {
            TryLockCamera();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        MovePlayer();
    }

    void MovePlayer() {
        Vector3 pos = Head.transform.position;
        Vector3 moveDirection = Vector3.up;
        moveDirection.x = Input.GetAxis("Horizontal");
        pos = Vector3.Lerp(pos, pos + moveDirection, GrowRate);
        pos.x = Mathf.Clamp(pos.x, LeftBounds, RightBounds);
        Head.transform.position = pos;
    }

    void TryLockCamera() {
        if (Head.transform.position.y >= CameraLockTrigger) {
            isCameraLocked = true;
            Camera.main.SendMessage("StartFollowPlayer", Head);
        }
    }
}
