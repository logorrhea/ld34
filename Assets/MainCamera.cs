using UnityEngine;
using System.Collections;


public class MainCamera : MonoBehaviour
{
    GameObject Player;
    bool isFollowingPlayer = false;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        if (isFollowingPlayer)
        {
            Vector3 pos = this.transform.position;
            this.transform.position = new Vector3(pos.x, Player.transform.position.y, pos.z);
        }
    }

    void StartFollowPlayer(GameObject player)
    {
        Player = player;
        isFollowingPlayer = true;
    }
}
