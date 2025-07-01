using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBush : MonoBehaviour
{
    public PlayerMovement pm;
    public float T = 30f;
    // Start is called before the first frame update
    void Start()
    {
        pm=GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pm.isHieding)
        {
            T -= Time.deltaTime;
            if (T <= 0f)
                Destroy(this.gameObject);
        }
    }
}
