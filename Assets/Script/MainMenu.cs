using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject PlayMode;
    // Start is called before the first frame update
    void Start()
{
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

    // Update is called once per frame
   
    /*    void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }*/

    public void Play()
    {
        PlayMode.SetActive(true);
    }
    public void BackPlay()
    {
        PlayMode.SetActive(false);
    }
    public void Story()
    {
        SceneManager.LoadScene("tutorial1");
    }
    public void Survival()
    {
        SceneManager.LoadScene("Survival");
    }
}
