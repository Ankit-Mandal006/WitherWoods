using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public GameObject creaturePrefab;
    public Transform[] spawnPoints;
    public int maxCreatures = 5;
    public float spawnDelay = 2f;

    private List<GameObject> activeCreatures = new List<GameObject>();
    private float timer;

    void Start()
    {
        for (int i = 0; i < maxCreatures; i++)
            SpawnCreature();
    }

    void Update()
    {
        // Clean up nulls
        activeCreatures.RemoveAll(c => c == null);

        // Spawn if under max, with delay
        if (activeCreatures.Count < maxCreatures)
        {
            timer += Time.deltaTime;
            if (timer >= spawnDelay)
            {
                SpawnCreature();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    void SpawnCreature()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject creature = Instantiate(creaturePrefab, point.position, point.rotation);
        activeCreatures.Add(creature);
    }
}
