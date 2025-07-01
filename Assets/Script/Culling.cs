using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culling : MonoBehaviour
{
    public GameObject player;
    public Terrain[] terrains;
    public float cullingDistance = 355f;
    private Dictionary<Terrain, bool> terrainStates = new Dictionary<Terrain, bool>();

    void Start()
    {
        foreach (Terrain terrain in terrains)
        {
            if (terrain == null) continue;

            // Store initial active state
            terrainStates[terrain] = true;
        }
        InvokeRepeating(nameof(UpdateCulling), 0f, 1f);
    }

    void UpdateCulling()
    {
        Vector3 playerPos = player.transform.position;

        foreach (Terrain terrain in terrains)
        {
            if (terrain == null) continue;

            float distance = Vector3.Distance(playerPos, terrain.transform.position);
            bool shouldBeActive = distance <= cullingDistance;

            // Update only if state changes
            if (terrainStates[terrain] != shouldBeActive)
            {
                terrainStates[terrain] = shouldBeActive;
                terrain.enabled = shouldBeActive;
                terrain.gameObject.SetActive(shouldBeActive);

                // Disable/Enable the terrain collider
                TerrainCollider terrainCollider = terrain.GetComponent<TerrainCollider>();
                if (terrainCollider != null)
                {
                    terrainCollider.enabled = shouldBeActive;
                }

                // Destroy objects on the terrain if it is deactivated
                if (!shouldBeActive)
                {
                    DestroyObjectsOnTerrain(terrain);
                }
            }
        }
    }

    void DestroyObjectsOnTerrain(Terrain terrain)
    {
        foreach (Transform child in terrain.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
