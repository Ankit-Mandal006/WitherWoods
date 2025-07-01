using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public Thundering th;
    public GameObject[] portals, p;
    public Terrain[] terrains;
    public Transform player;
    public float playerAvoidRadius = 50f;
    public float minDistanceBetweenPortals = 5f;
    public int t = 300;

    private int i = 0;
    private List<Vector3> occupiedPositions = new List<Vector3>();
    private HashSet<int> usedIndices = new HashSet<int>();

    void Start()
    {
        StartCoroutine(SpawnPortalsWithDelay());
        InvokeRepeating("UpdatePortals", 30f, 1f);
    }

    IEnumerator SpawnPortalsWithDelay()
    {
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(SpawnPortals());
        yield return new WaitForSeconds(10f);
        StartCoroutine(ReLocateLoop());
    }

    void UpdatePortals()
    {
        for (int i = 0; i < portals.Length; i++)
        {
            if (portals[i] == null)
                Destroy(p[i]);
        }
    }

    IEnumerator SpawnPortals()
    {
        usedIndices.Clear();
        int playerTerrainIndex = GetPlayerTerrainIndex();

        for (int j = 0; j < portals.Length; j++)
        {
            int selectedIndex;
            do
            {
                selectedIndex = Random.Range(0, terrains.Length);
            }
            while (usedIndices.Contains(selectedIndex) ||
                   selectedIndex == playerTerrainIndex ||
                   usedIndices.Contains((selectedIndex - 1 + terrains.Length) % terrains.Length) ||
                   usedIndices.Contains((selectedIndex + 1) % terrains.Length));

            usedIndices.Add(selectedIndex);
            Terrain selectedTerrain = terrains[selectedIndex];

            Vector3 spawnPos;
            int attempts = 0;
            do
            {
                spawnPos = GetFlatPosition(selectedTerrain);
                attempts++;
                if (attempts > 20) break;
            }
            while (IsTooCloseToOthers(spawnPos));

            occupiedPositions.Add(spawnPos);
            GameObject instance = Instantiate(portals[j], spawnPos, Quaternion.identity);
            th.portal = instance.transform;
            th.thunder = true;
            p[j] = instance;

            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator ReLocateLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(t);

            if (p[i] != null)
            {
                int playerTerrainIndex = GetPlayerTerrainIndex();
                int selectedIndex;
                do
                {
                    selectedIndex = Random.Range(0, terrains.Length);
                }
                while (selectedIndex == playerTerrainIndex ||
                       usedIndices.Contains((selectedIndex - 1 + terrains.Length) % terrains.Length) ||
                       usedIndices.Contains((selectedIndex + 1) % terrains.Length));

                Terrain selectedTerrain = terrains[selectedIndex];

                Vector3 newPos;
                int attempts = 0;
                do
                {
                    newPos = GetFlatPosition(selectedTerrain);
                    attempts++;
                    if (attempts > 20) break;
                }
                while (IsTooCloseToOthers(newPos, i));

                occupiedPositions[i] = newPos;
                p[i].transform.position = newPos;
                th.portal = p[i].transform;
                th.thunder = true;
            }

            i = (i + 1) % p.Length;
        }
    }

    Vector3 GetFlatPosition(Terrain terrain)
    {
        TerrainData data = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;
        for (int attempt = 0; attempt < 100; attempt++)
        {
            float x = Random.Range(0f, data.size.x);
            float z = Random.Range(0f, data.size.z);
            float worldX = terrainPos.x + x;
            float worldZ = terrainPos.z + z;
            float height = data.GetHeight((int)x, (int)z);

            if (height < 0.9f) continue;

            if (Vector3.Distance(new Vector3(worldX, 0, worldZ), new Vector3(player.position.x, 0, player.position.z)) < playerAvoidRadius)
                continue;

            if (!IsAreaFlatEnough(data, x, z, 5f)) continue;

            return new Vector3(worldX, 0.95f, worldZ);
        }
        return terrain.transform.position + new Vector3(data.size.x / 2, 0.95f, data.size.z / 2);
    }

    bool IsAreaFlatEnough(TerrainData data, float x, float z, float radius)
    {
        float centerHeight = data.GetHeight((int)x, (int)z);
        for (float dx = -radius; dx <= radius; dx += 2f)
        {
            for (float dz = -radius; dz <= radius; dz += 2f)
            {
                float checkX = Mathf.Clamp(x + dx, 0, data.heightmapResolution - 1);
                float checkZ = Mathf.Clamp(z + dz, 0, data.heightmapResolution - 1);
                float h = data.GetHeight((int)checkX, (int)checkZ);
                if (Mathf.Abs(h - centerHeight) > 0.1f)
                    return false;
            }
        }
        return true;
    }

    bool IsTooCloseToOthers(Vector3 pos, int skipIndex = -1)
    {
        for (int k = 0; k < occupiedPositions.Count; k++)
        {
            if (k == skipIndex) continue;
            if (Vector3.Distance(pos, occupiedPositions[k]) < minDistanceBetweenPortals)
                return true;
        }
        return false;
    }

    int GetPlayerTerrainIndex()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit, 10f))
        {
            for (int i = 0; i < terrains.Length; i++)
            {
                if (terrains[i].gameObject == hit.collider.gameObject)
                    return i;
            }
        }
        return -1;
    }
}
