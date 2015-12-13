using UnityEngine;
using System.Collections;

public class Leaf : MonoBehaviour
{
    public float AnimationSpeed;

    float Scale = 0.0f;
    float MaxScale;
    bool FinishedAnimating = false;

    // Use this for initialization
    void Start ()
    {
        if (Random.Range(1, 3) == 2)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.flipX = true;
        }
        MaxScale = Random.Range(0.6f, 1.0f);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!FinishedAnimating)
        {
            Scale = Mathf.Clamp(Mathf.LerpAngle(Scale, Scale+0.1f, AnimationSpeed), 0.0f, MaxScale);
            this.transform.localScale = new Vector3(Scale, Scale, 1.0f);
            if (Mathf.Approximately(Scale, MaxScale))
            {
                FinishedAnimating = true;
            }
        }

        Bounds b = CameraExtensions.OrthographicBounds(Camera.main);
        if (GameState.isPaused ||
            this.transform.position.y < b.min.y ||
            this.transform.position.y > b.max.y)
        {
            Destroy(this.gameObject);
        }
    }
}
