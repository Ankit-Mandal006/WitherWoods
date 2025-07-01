using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActive_After_Teleport : MonoBehaviour
{
    public bool isTrue=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrue)
            Invoke("Destroy_obj",5f);
    }
    void Destroy_obj()
    {
        Destroy(this.gameObject);
    }
    
}
