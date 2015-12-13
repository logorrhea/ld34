using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float MaxRotationAngle;
    public float RotationSpeed;
    public float CameraLockTrigger = 0.4f;
    public GameObject Head;
    public float GrowRate;
    public Text HeightCounter;
    public GameObject Spawner;

    public GameObject DeathMenu;
    public Text CurrentRoundHeightText;
    public Text RecordHeightText;

    Vector3 Origin;
    Vector3 CameraOrigin;
    float LeftBounds, RightBounds;
    bool isCameraLocked = false;
    bool isStopped = false;

    // Use this for initialization
    void Start ()
    {
        // Save original position of player and camera
        Origin = Head.transform.position;
        CameraOrigin = Camera.main.transform.position;

        // Get right and left screen bounds
        Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
        LeftBounds = bounds.min.x;
        RightBounds = bounds.max.x;

        // Turn off DeathMenu
        DeathMenu.SetActive(false);
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
        if (!GameState.isPaused)
        {
            MovePlayer();
            GrowLeaves();
        }
    }

    void MovePlayer()
    {
        // Move player based on keys pressed
        Vector3 pos = Head.transform.position;
        Vector3 moveDirection = Vector3.up;
        moveDirection.x = Input.GetAxis("Horizontal");
        pos = Vector3.Lerp(pos, pos + moveDirection, GrowRate);
        pos.x = Mathf.Clamp(pos.x, LeftBounds, RightBounds);
        Head.transform.position = pos;

        // Passively rotate player
        Vector3 angles =  Head.transform.rotation.eulerAngles;
        float newAngle = Mathf.Sin(Time.time)*MaxRotationAngle;
        Head.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(angles.z, newAngle, RotationSpeed));

        // Update height counter text
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
            Spawner.SendMessage("StartSpawningUnits");
        }
    }

    void Die()
    {
        // Tell camera to stop tracking the player
        Camera.main.SendMessage("StopFollowPlayer");
        isCameraLocked = false;

        // Tell unit spawner to stop spawning units
        // and delete the ones it currently has spawned
        Spawner.SendMessage("StopSpawningUnits");

        // Stop movement (set isDead variable?)
        GameState.isPaused = true;

        // Show Overlay w/ current height reached, and maximum height reached
        UpdateDeathMenu(Head.transform.position.y);
        DeathMenu.SetActive(true);

        // Set self to inactive
        Head.SetActive(false);
    }

    public void Restart()
    {
        // Reset player and camera positions
        Head.transform.position = Origin;
        Camera.main.transform.position = CameraOrigin;

        // Turn off DeathMenu
        DeathMenu.SetActive(false);

        // Set self to active
        Head.SetActive(true);

        // Unpause game
        GameState.isPaused = false;
    }

    void UpdateHeightCounter(float height)
    {
        height = height/10.0f; // height will be in 10cm
        HeightCounter.text = height.ToString("F2") + " m";
    }

    void UpdateDeathMenu(float height)
    {
        bool newHighestHeight = false;
        height = height / 10.0f;
        if (height > GameState.RecordHeight)
        {
            newHighestHeight = true;
            GameState.RecordHeight = height;
        }

        CurrentRoundHeightText.text = "Height: " + height.ToString("F2") + "m";
        if (newHighestHeight)
        {
            RecordHeightText.text = "New Record Height!";
        }
        else
        {
            RecordHeightText.text = "Record Height: " + GameState.RecordHeight.ToString("F2") + "m";
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        Die();
    }
}
