using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public Animator anim;
    public int count = 0, last_count = 0;
    public GameObject panel, deathScreen;
    public float T = 3f;

    private bool isInvincible = false;
    public float invincibilityDuration = 1.5f;

    void Update()
    {
        // Check if count has changed
        if (last_count != count)
        {
            if (isInvincible)
            {
                // Prevent count increase during invincibility
                count = last_count;
            }
            else
            {
                // Process the hit normally
                StartCoroutine(Process());
                last_count = count;
            }
        }
    }

    IEnumerator Process()
    {
        isInvincible = true;
        StartCoroutine(InvincibilityCooldown());

        yield return new WaitForSecondsRealtime(0f);
        panel.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(DelayPanelOff());

        if (count == 1)
            anim.SetTrigger("4");
        else if (count == 2)
            anim.SetTrigger("3");
        else if (count == 3)
            anim.SetTrigger("2");
        else if (count == 4)
            anim.SetTrigger("1");
        else if (count == 5)
        {
            anim.SetTrigger("0");
            StartCoroutine(DelayDeathScreen());
        }
    }

    IEnumerator DelayPanelOff()
    {
        yield return new WaitForSecondsRealtime(2f);
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    IEnumerator DelayDeathScreen()
    {
        yield return new WaitForSecondsRealtime(2f);
        deathScreen.SetActive(true);
    }

    IEnumerator InvincibilityCooldown()
    {
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}
