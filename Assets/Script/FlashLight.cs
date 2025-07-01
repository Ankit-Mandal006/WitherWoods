using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashLight : MonoBehaviour
{
    public Animator anim;
    public GameObject flashLight;
    public bool on = true;
    public bool off = false;

    public RawImage uiImage;
    public TMP_Text uiText;

    void Update()
    {
        if (on && Input.GetButtonDown("F"))
        {
            anim.SetBool("holdLight", false);
            flashLight.SetActive(false);
            SetUIAlpha(0.05f);
            off = true;
            on = false;
        }
        else if (off && Input.GetButtonDown("F"))
        {
            anim.SetBool("holdLight", true);
            flashLight.SetActive(true);
            SetUIAlpha(1f);
            off = false;
            on = true;
        }
    }

    void SetUIAlpha(float alpha)
    {
        if (uiImage != null)
        {
            Color c = uiImage.color;
            c.a = alpha;
            uiImage.color = c;
        }

        if (uiText != null)
        {
            Color c = uiText.color;
            c.a = alpha;
            uiText.color = c;
        }
    }
}
