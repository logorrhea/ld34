using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    public Sprite[] CloudSprites;
    public float MaxActiveClouds;
    public GameObject CloudPrefab;
    public float CloudSpawnChance;
    public float CloudSpawnDelay;

    List<GameObject> ActiveClouds;
    float TimeSinceLastCloud;

    // Use this for initialization
    void Start ()
    {
        TimeSinceLastCloud = CloudSpawnDelay;
        ActiveClouds = new List<GameObject>();
    }

    // Update is called once per frame
    void Update ()
    {
        int rand = Random.Range(0, 101);
        if (ActiveClouds.Count < MaxActiveClouds &&
            TimeSinceLastCloud > CloudSpawnDelay &&
            rand < CloudSpawnChance)
        {
            Sprite sprite = CloudSprites[Random.Range(0, CloudSprites.Length)];
            GameObject cloud = (GameObject) Instantiate(CloudPrefab);
            cloud.transform.parent = Camera.main.transform;
            ActiveClouds.Add(cloud);
            cloud.SendMessage("SetSpawner", this.gameObject);
        }

        TimeSinceLastCloud += Time.deltaTime;
    }

    public void RemoveCloud(GameObject Cloud)
    {
        ActiveClouds.Remove(Cloud);
        Destroy(Cloud);
    }
}
