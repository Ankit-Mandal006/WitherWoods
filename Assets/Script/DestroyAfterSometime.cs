using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSometime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroy",time);
    }
    void destroy()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
