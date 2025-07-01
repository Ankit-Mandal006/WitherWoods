using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeTimer : MonoBehaviour
{
    public WaveContoller wc;
    public Thundering th;
    public PortalSpawner ps;
    public TMP_Text timer_text;
    public bool startTimer;
    public Terrain[] terrains;
    public float Timer = 100f;
    public GameObject player,portals;
    public Transform spawnPoint;
    private List<Vector3> occupiedPositions = new List<Vector3>();
    void Start()
    {
        terrains=ps.terrains;
    }
    void Update()
    {
        if (startTimer && Timer > 0)
        {
            Timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timer / 60);
            int seconds = Mathf.FloorToInt(Timer % 60);
            timer_text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        if (Timer <= 0)
        {
            wc.count = 4;
            Timer = 100f;
            startTimer=false;
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            player.transform.position = spawnPoint.position;
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
            ps.p[1] = instance;
            Invoke("ReactivateController", 0.1f);
        }
    }
    void LateUpdate() {
        if (!startTimer)
            timer_text.text = "";
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
