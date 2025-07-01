using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBheaviour : MonoBehaviour
{
    public GameObject crystal;
    public PlayerInventory pi;
    public bool crystalDroped=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(crystalDroped)
        {
            crystal.SetActive(true);
            //Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        
    }
}
