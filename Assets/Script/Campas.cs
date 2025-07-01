using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Campas : MonoBehaviour
{
    public GameObject player;
    public RawImage Compas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Compas.rectTransform.localEulerAngles = new Vector3(0, 0, -player.transform.eulerAngles.y);
    }
}
