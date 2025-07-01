using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyCurve : MonoBehaviour
{
    public ActivateMainPortal amp;
    public GameObject creep1,creep2,creep3,A,B;
    public Shooting_Mechanics sm;

    void Update()
    {
        if (amp.count1 == 1)
        {
            creep1.SetActive(true);
            A.SetActive(true);
             B.SetActive(true);
            sm.maxAmmo = 30f;
            sm.enabled = true;
        }
        if(amp.count1==2)
        {
            creep2.SetActive(true);
        }
        if(amp.count1==3)
        {
            creep3.SetActive(true);
        }
    }
}
