using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] Units;
    public Transform SpawnPoint;
    public float SpawnRate;

    List<GameObject> SpawnedUnits;

    float LeftBounds, RightBounds;

    // Use this for initialization
    void Start ()
    {
        SpawnedUnits = new List<GameObject>();
        Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
        LeftBounds = bounds.min.x;
        RightBounds = bounds.max.x;
    }

    public void StartSpawningUnits()
    {
        StartCoroutine(RepeatSpawnUnits());
    }

    public void StopSpawningUnits()
    {
        StopCoroutine(RepeatSpawnUnits());
        foreach (GameObject g in SpawnedUnits)
        {
            Destroy(g);
        }
        SpawnedUnits.Clear();
    }

    private IEnumerator RepeatSpawnUnits()
    {
        while(!GameState.isPaused)
        {
            float u = SpawnUnit();
            yield return new WaitForSeconds(u);
        }
    }

    private float SpawnUnit()
    {
        GameObject unit = Units[Random.Range(0, Units.Length)];
        Vector3 spawnPoint = SpawnPoint.position;
        spawnPoint.x = Random.Range(LeftBounds, RightBounds);
        GameObject newUnit = (GameObject)Instantiate(unit, spawnPoint, Quaternion.identity);
        SpawnedUnits.Add(newUnit);
        return SpawnRate;
    }
}
