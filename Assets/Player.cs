using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public GameObject Controller;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        Controller.SendMessage("Die");
    }

    public void SetController(GameObject Controller)
    {
        this.Controller = Controller;
    }
}
