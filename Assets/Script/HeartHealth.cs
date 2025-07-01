using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealth : MonoBehaviour
{
    public KillableEnemy ke;
    public Image image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = ke.health / ke.totalhealth;    
    }
}
