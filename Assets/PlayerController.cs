using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public GameObject head;
    public float growRate;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void FixedUpdate () {
        MovePlayer();
    }

    void MovePlayer() {
        Vector3 pos = head.transform.position;
        pos.y += growRate;
        head.transform.position = pos;
    }
}
