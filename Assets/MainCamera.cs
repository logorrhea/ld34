using UnityEngine;
using System.Collections;


public class MainCamera : MonoBehaviour
{
    GameObject Player;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        if (GameState.isFollowingPlayer)
        {
            Vector3 pos = this.transform.position;
            this.transform.position = new Vector3(pos.x, Player.transform.position.y, pos.z);
        }
    }

    void StartFollowPlayer(GameObject player)
    {
        Player = player;
        GameState.isFollowingPlayer = true;
    }

    void  StopFollowPlayer()
    {
        GameState.isFollowingPlayer = false;
    }
}
