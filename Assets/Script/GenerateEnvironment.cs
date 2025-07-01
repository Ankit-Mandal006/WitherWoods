using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    public GameObject[] trees;         // Tree prefabs
    public Transform player;           // Player reference
    public int maxTrees = 20;          // Maximum number of trees
    public float innerRadius = 5f;     // Minimum spawn distance from player
    public float outerRadius = 20f;    // Maximum spawn distance
    public float despawnDistance = 25f; // Distance to remove trees
    public float checkInterval = 0.1f; // Spawn check interval (every 0.1 sec)

    private List<GameObject> activeTrees = new List<GameObject>(); // Active trees
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    //private bool isFirstSpawn = true;

    void Start()
    {
        SpawnTrees(maxTrees, false); // Spawn first batch
        lastPosition = player.position;
        lastRotation = player.rotation;
        StartCoroutine(TreeSpawnRoutine());
    }

    IEnumerator TreeSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            if (PlayerMovedOrRotated())
            {
                RemoveFarTrees();
                if (activeTrees.Count < maxTrees)
                {
                    SpawnTrees(1, true);
                }
            }
        }
    }

    bool PlayerMovedOrRotated()
    {
        bool moved = Vector3.Distance(player.position, lastPosition) > 0.1f;
        bool rotated = Quaternion.Angle(player.rotation, lastRotation) > 1f;

        if (moved || rotated)
        {
            lastPosition = player.position;
            lastRotation = player.rotation;
            return true;
        }
        return false;
    }

    void SpawnTrees(int amount, bool inFrontOfCamera)
    {
        int spawned = 0;
        while (spawned < amount)
        {
            Vector3 randomPosition = GetRandomPosition(inFrontOfCamera);

            if (IsFarEnough(randomPosition))
            {
                GameObject treePrefab = trees[Random.Range(0, trees.Length)];
                GameObject newTree = Instantiate(treePrefab, randomPosition, Quaternion.identity);
                activeTrees.Add(newTree);
                spawned++;
            }
        }
    }

    Vector3 GetRandomPosition(bool inFrontOfCamera)
    {
        if (inFrontOfCamera)
        {
            Vector3 forward = player.forward.normalized;
            float randomAngle = Random.Range(-30f, 30f);
            Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
            Vector3 direction = rotation * forward;

            float distance = Random.Range(innerRadius, outerRadius);
            return player.position + direction * distance;
        }
        else
        {
            float randomAngle = Random.Range(0f, 360f);
            float distance = Random.Range(innerRadius, outerRadius);
            return player.position + new Vector3(
                Mathf.Cos(randomAngle * Mathf.Deg2Rad) * distance,
                0f,
                Mathf.Sin(randomAngle * Mathf.Deg2Rad) * distance
            );
        }
    }

    bool IsFarEnough(Vector3 position)
    {
        foreach (GameObject tree in activeTrees)
        {
            if (tree != null && Vector3.Distance(position, tree.transform.position) < innerRadius)
                return false;
        }
        return true;
    }

    void RemoveFarTrees()
    {
        for (int i = activeTrees.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(player.position, activeTrees[i].transform.position) > despawnDistance)
            {
                Destroy(activeTrees[i]);
                activeTrees.RemoveAt(i);
            }
        }
    }
}
