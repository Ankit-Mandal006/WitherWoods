using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReChangeDoor : MonoBehaviour
{
    public InfiniteDoorWay idw;
    // Start is called before the first frame update
    void Start()
    {
        idw=GameObject.Find("InfiniteDoorWay").GetComponent<InfiniteDoorWay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            idw.SetPortalConnection();
        }
    }
}
