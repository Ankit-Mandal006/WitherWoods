using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderingManager : MonoBehaviour
{
    public Thundering th;
    public PortalSpawner ps;

    void Start()
    {
        InvokeRepeating("UpdatePortal", 0f, 10f);
    }

    void UpdatePortal()
    {
        List<int> validIndices = new List<int>();

        for (int i = 0; i < ps.p.Length; i++)
        {
            if (ps.p[i] != null)
                validIndices.Add(i);
        }

        if (validIndices.Count > 0)
        {
            int randomIndex = validIndices[Random.Range(0, validIndices.Count)];
            th.portal = ps.p[randomIndex].transform;
        }
    }
}
