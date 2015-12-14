using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{
    public float MaxMoveSpeed;
    public float MinMoveSpeed;
    public GameObject Spawner;

    float MoveSpeed;
    int MoveDirection;
    float LeftBound;
    float RightBound;

    float ScreenHeight;
    float ScreenWidth;

    float SpriteWidth;
    float SpriteHeight;

    float EndPosition;

    void Start ()
    {
        MoveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);

        // Determine bounds
        Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
        LeftBound = bounds.min.x;
        RightBound = bounds.max.x;
        ScreenHeight = Screen.height/(Camera.main.orthographicSize*2*10);
        ScreenWidth = Screen.width/(Camera.main.orthographicSize*2*10);

        // Determine start position and movement direction
        MoveDirection = (Random.Range(0, 2) == 0) ? -1 : 1;

        // Store Sprite width
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        SpriteWidth = sr.sprite.rect.width;
        SpriteHeight = sr.sprite.rect.height;

        // Determine start position
        float x = MoveDirection == 1 ? LeftBound - SpriteWidth/200 : RightBound + SpriteWidth/200;
        Vector3 startPosition = new Vector3(x, Random.Range(-ScreenHeight/2, ScreenHeight), 10);
        this.transform.localPosition = startPosition;

        // Determine end position
        EndPosition = MoveDirection == 1 ? RightBound + SpriteWidth/100 : LeftBound - SpriteWidth/100;
    }

    void FixedUpdate ()
    {
        Vector3 pos = this.transform.position;
        pos.x += MoveSpeed * MoveDirection;

        // If the camera is tracking the player, subtly move the clouds downward
        // for like some parallax action
        if (GameState.isFollowingPlayer)
        {
            pos.y -= 1.0f;
        }
        this.transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);

        if ((MoveDirection == 1 && pos.x >= EndPosition) ||
            (MoveDirection == -1 && pos.x <= EndPosition))
        {
            Spawner.SendMessage("RemoveCloud", this.gameObject);
        }
    }

    public void SetSpawner(GameObject Spawner)
    {
        this.Spawner = Spawner;
    }
}
