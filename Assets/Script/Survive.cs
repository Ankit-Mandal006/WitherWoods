using System.Collections;
using UnityEngine;

public class Survive : MonoBehaviour
{
    public GameObject player, panel;
    public SurvivalTimer st;
    private bool hasActivated = false; // Prevent repeated triggers
    void Start()
    {
        if (hasActivated) return;
        /*
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance <= 2f)
        {*/
            hasActivated = true;

            StartCoroutine(ShowPanelAfterRealtime(2f));
            st.A = false;
            //Time.timeScale = 0f;// Assuming you use this to stop the timer
        //}
    }
    void LateUpdate()
    {
        
    }

    IEnumerator ShowPanelAfterRealtime(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        panel.SetActive(true);
    }
}
