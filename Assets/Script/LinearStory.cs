using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearStory : MonoBehaviour
{
    public PlayerInventory pi;
    int count = 0;
    public PortalSpawner ps;
    // Start is called before the first frame update
    void Start()
    {
        ps.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pi.hasCrystal)
            count = 1;
        if (count == 1)
        {
            ps.enabled = true;
        }
    }
}
