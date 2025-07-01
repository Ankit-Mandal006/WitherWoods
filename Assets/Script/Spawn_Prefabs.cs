using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Prefabs : MonoBehaviour
{
    public GameObject[] prefabs, terrain;
    public Transform player;
    private GameObject playerTerrain = null;

    void Start()
    {
        InvokeRepeating(nameof(UpdateSpawner), 0f, 1f);
    }

    void UpdateSpawner()
    {
        playerTerrain = GetPlayerTerrain();
        for (int i = 0; i < terrain.Length; i++)
        {
            if (terrain[i].activeSelf && terrain[i] != playerTerrain && terrain[i].transform.childCount <= 0)
            {
                Vector3 location = RandomLocation(terrain[i]);
                if (location != Vector3.zero)
                {
                    GameObject currentPrefab = Instantiate(prefabs[Random.Range(0, prefabs.Length)], terrain[i].transform);
                    currentPrefab.transform.localPosition = location;
                }
            }
        }
    }

    Vector3 RandomLocation(GameObject currentTerrain)
    {
        for (int attempts = 0; attempts < 10; attempts++)
        {
            Vector3 localPos = new Vector3(Random.Range(0, 50), 0, Random.Range(0, 50));
            Vector3 worldPos = currentTerrain.transform.TransformPoint(localPos);
            float height = SampleTerrainHeight(currentTerrain, worldPos);

            if (height > 0.8f)
            {
                Collider[] hitColliders = Physics.OverlapSphere(worldPos, 0.5f);
                bool isBlocked = false;
                foreach (Collider hit in hitColliders)
                {
                    if (hit.CompareTag("Untagged"))
                    {
                        isBlocked = true;
                        break;
                    }
                }
                if (!isBlocked)
                {
                    localPos.y = height;
                    return localPos;
                }
            }
        }
        return Vector3.zero;
    }

    float SampleTerrainHeight(GameObject terrainObj, Vector3 worldPos)
    {
        Terrain terrain = terrainObj.GetComponent<Terrain>();
        if (terrain != null)
            return terrain.SampleHeight(worldPos);
        return worldPos.y;
    }

    GameObject GetPlayerTerrain()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit, 5f))
            return hit.collider.gameObject;
        return null;
    }
}
