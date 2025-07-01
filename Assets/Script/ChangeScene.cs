using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public string s="MainMenu";
    public float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("A", t);
    }
    void A()
    {
    SceneManager.LoadScene(s);
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
