using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float CameraLockTrigger = 0.4f;
    public GameObject Head;
    public float GrowRate;

    float AxisMin = 0.2f;

    bool isCameraLocked = false;

    // Use this for initialization
    void Start () {
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
        Head.transform.position = Vector3.Lerp(pos, pos + moveDirection, GrowRate);
    }

    void TryLockCamera() {
        if (Head.transform.position.y >= CameraLockTrigger) {
            isCameraLocked = true;
            Camera.main.SendMessage("StartFollowPlayer", Head);
        }
    }
}
