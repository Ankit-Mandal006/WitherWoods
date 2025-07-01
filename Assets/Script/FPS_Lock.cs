using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Lock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        QualitySettings.vSyncCount = 0; // Completely turn off V-Sync
        Application.targetFrameRate = 60; // Manually set the frame rate
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
