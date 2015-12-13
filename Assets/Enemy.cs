using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float PatrolRange;
    public float PatrolSpeed;

    float StartX;
    float OscillationOffset;

    // Use this for initialization
    void Start ()
    {
        StartX = this.transform.position.x;
        OscillationOffset = Time.time;
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 pos = this.transform.position;
        float x = StartX + Mathf.Sin(OscillationOffset + Time.time)*PatrolRange;
        pos.x = Mathf.Lerp(pos.x, x, PatrolSpeed);
        this.transform.position = pos;
    }

    void LateUpdate()
    {
        Bounds b = CameraExtensions.OrthographicBounds(Camera.main);
        if (this.transform.position.y < b.min.y)
        {
            Destroy(this.gameObject);
        }
    }
}
