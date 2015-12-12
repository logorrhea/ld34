using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] Units;
    public Transform SpawnPoint;
    public float SpawnRate;

    float LeftBounds, RightBounds;

    // Use this for initialization
    void Start ()
    {
        Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
        LeftBounds = bounds.min.x;
        RightBounds = bounds.max.x;
        StartCoroutine(RepeatSpawnUnits());
    }

    private IEnumerator RepeatSpawnUnits()
    {
        while(true) {
            float u = SpawnUnit();
            yield return new WaitForSeconds(u);
        }
    }

    private float SpawnUnit()
    {
        GameObject unit = Units[Random.RandomRange(0, Units.Length)];
        Vector3 spawnPoint = SpawnPoint.position;
        spawnPoint.x = Random.RandomRange(LeftBounds, RightBounds);
        GameObject go = (GameObject) Instantiate(unit, spawnPoint, Quaternion.identity);
        return SpawnRate;
    }
}
