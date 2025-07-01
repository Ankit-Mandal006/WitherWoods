using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnStepping : MonoBehaviour
{
    public AudioSource dialouge;
    public GameObject dialougeText;
    int z = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dialouge.isPlaying)
            dialougeText.SetActive(true);
        else
            dialougeText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (z == 0)
            {
                Invoke("A", 2f);

            }
            z = 1;
        }
    }
    void A()
    {
        dialouge.Play();
        Invoke("B", 10f);
    }
    void B()
    {
        Destroy(this);
    }
}
