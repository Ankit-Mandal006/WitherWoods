using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    float c = 0f;
    public TMP_Text timer_text;
    public TMP_Text final;
    public bool A = true;

    // Update is called once per frame
    void Update()
    {
        if(A)
        c += Time.deltaTime;

        int minutes = Mathf.FloorToInt(c / 60f);
        int seconds = Mathf.FloorToInt(c % 60f);

        timer_text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        final.text = timer_text.text;
    }
}
