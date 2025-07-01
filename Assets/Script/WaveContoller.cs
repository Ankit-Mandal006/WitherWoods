using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveContoller : MonoBehaviour
{
    public GameObject Wave1,Wave2,Wave3;
    public Thundering th;
    public PortalSpawner ps;
    public Terrain[] terrains;
    public GameObject player,portals;
    public Transform spawnPoint;
    private List<Vector3> occupiedPositions = new List<Vector3>();
    public int count=1;
    // Start is called before the first frame update
    void Start()
    {
        terrains=ps.terrains;
    }
    void A()
    {
        player.transform.position = spawnPoint.position;
    }
    // Update is called once per frame
   private bool hasHandledWave4 = false;

void Update()
{
    if (count == 1)
    {
        Wave1.SetActive(true);
        Wave2.SetActive(false);
        Wave3.SetActive(false);
        hasHandledWave4 = false;
    }
    else if (count == 2)
    {
        Wave2.SetActive(true);
        Wave1.SetActive(false);
        Wave3.SetActive(false);
        hasHandledWave4 = false;
    }
    else if (count == 3)
    {
        Wave3.SetActive(true);
        Wave1.SetActive(false);
        Wave2.SetActive(false);
        hasHandledWave4 = false;
    }
    else if (count == 4 && !hasHandledWave4)
    {
        hasHandledWave4 = true; // ✅ Prevent repeated execution

        player.GetComponent<Animator>().SetTrigger("SpriteJumpScare");
        player.GetComponent<PlayerMovement>().enabled = false;

        GameObject cam = GameObject.FindWithTag("MainCamera");
        cam.GetComponent<MouseLook>().enabled = false;
        cam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        Invoke(nameof(Reset), 3f);
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;
        Invoke(nameof(A), 0.2f);

        // Spawn logic
        HashSet<int> usedIndices = new HashSet<int>();
        int selectedIndex;
        do
        {
            selectedIndex = Random.Range(0, terrains.Length);
        }
        while (usedIndices.Contains(selectedIndex) ||
               usedIndices.Contains((selectedIndex - 1 + terrains.Length) % terrains.Length) ||
               usedIndices.Contains((selectedIndex + 1) % terrains.Length));

        usedIndices.Add(selectedIndex);
        Terrain selectedTerrain = terrains[selectedIndex];

        Vector3 spawnPos;
        do
        {
            spawnPos = GetSafeRandomPoint(selectedTerrain);
        }
        while (IsTooCloseToOthers(spawnPos));

        occupiedPositions.Add(spawnPos);

        GameObject instance = Instantiate(portals, spawnPos, Quaternion.identity);
        th.portal = instance.transform;
        th.thunder = true;
        ps.p[0] = instance;

        Invoke(nameof(ReactivateController), 0.3f);

        count = 1; // ✅ Now reset safely after handling
    }
}

    void Reset()
    {
        //player.GetComponent<Animator>().SetTrigger("SpriteJumpScare");
        player.GetComponent<PlayerMovement>().enabled=true;
        GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>().enabled=true;
        //cam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
    Vector3 GetSafeRandomPoint(Terrain terrain)
    {
        Vector3 point;
        do
        {
            point = GetRandomPointOnTerrain(terrain);
        }
        while (Vector3.Distance(point, player.transform.position) < 20f);

        return point;
    }

    Vector3 GetRandomPointOnTerrain(Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;
        float x = Random.Range(0, terrainData.size.x);
        float z = Random.Range(0, terrainData.size.z);
        float y = terrainData.GetHeight((int)x, (int)z) + terrain.transform.position.y;

        return new Vector3(x + terrain.transform.position.x, y, z + terrain.transform.position.z);
    }

    bool IsTooCloseToOthers(Vector3 pos, int skipIndex = -1)
    {
        for (int k = 0; k < occupiedPositions.Count; k++)
        {
            if (k == skipIndex) continue;
            if (Vector3.Distance(pos, occupiedPositions[k]) < 100)
                return true;
        }
        return false;
    }
    void ReactivateController()
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = true;
    }
}
