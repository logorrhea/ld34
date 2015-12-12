using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    public float CameraLockTrigger = 0.4f;
    public GameObject Head;
    public float GrowRate;
    public Text HeightCounter;

    float LeftBounds, RightBounds;
    float AxisMin = 0.2f;
    bool isCameraLocked = false;

    // Use this for initialization
    void Start ()
    {
        Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
        LeftBounds = bounds.min.x;
        RightBounds = bounds.max.x;
    }

    void Update()
    {
        if (!isCameraLocked)
        {
            TryLockCamera();
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        MovePlayer();
        GrowLeaves();
    }

    void MovePlayer()
    {
        Vector3 pos = Head.transform.position;
        Vector3 moveDirection = Vector3.up;
        moveDirection.x = Input.GetAxis("Horizontal");
        pos = Vector3.Lerp(pos, pos + moveDirection, GrowRate);
        pos.x = Mathf.Clamp(pos.x, LeftBounds, RightBounds);
        Head.transform.position = pos;

        UpdateHeightCounter(pos.y);
    }

    void GrowLeaves()
    {
        // @TODO: Occasionally grow leafy bits based on change in height/random chance
    }

    void TryLockCamera()
    {
        if (Head.transform.position.y >= CameraLockTrigger)
        {
            isCameraLocked = true;
            Camera.main.SendMessage("StartFollowPlayer", Head);
        }
    }

    void UpdateHeightCounter(float height)
    {
        height = height/10.0f; // height will be in 10cm
        HeightCounter.text = height.ToString("F2") + " m";
    }
}
