using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject heart, endScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heart == null)
        {
            endScreen.SetActive(true);
            Time.timeScale = 0f;
        }    
    }
}
