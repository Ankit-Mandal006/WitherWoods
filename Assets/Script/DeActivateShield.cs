using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActivateShield : MonoBehaviour
{
    public Thundering th;
    public GameObject shield;
    public GameObject orbPrefab;
    public Vector2 areaSize = new Vector2(20, 20);
    public int orbCount = 10;
    public LayerMask obstacleMask;
    void Start()
    {
        Invoke("SpawnOrbs", 3f);
    }
    public void DeActivate()
    {
        shield.SetActive(false);
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(50f); // Waits 50 seconds
        shield.SetActive(true);
        SpawnOrbs();
    }

    public void SpawnOrbs()
    {
        int spawned = 0;
        int attempts = 0;

        while (spawned < orbCount && attempts < orbCount * 20)
        {
            attempts++;
            Vector3 pos = GetRandomPosition();
            if (IsValidPosition(pos))
            {
                GameObject instance =Instantiate(orbPrefab, pos, Quaternion.identity);
                th.portal = instance.transform;
                th.thunder = true;
                spawned++;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector2 offset = new Vector2(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );
        Vector3 center = transform.position;
        return new Vector3(center.x + offset.x, 1.52f, center.z + offset.y); // Spawn above terrain
    }

    bool IsValidPosition(Vector3 pos)
    {
        // Raycast down to terrain
        if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit, 100f))
        {
            Vector3 groundPos = hit.point;

            // Check for obstacles
            if (Physics.CheckSphere(groundPos, 0.5f, obstacleMask)) return false;

            // Check exclusion radius (50 units from this spawner)
            if (Vector3.Distance(groundPos, transform.position) < 50f) return false;

            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Spawn area in green
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, 1, areaSize.y));

        // Exclusion zone in red
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 50f);
    }
}
