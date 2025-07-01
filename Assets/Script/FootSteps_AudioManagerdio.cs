using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps_AudioManager : MonoBehaviour
{
    public bool AlwaysTrue = false;
    [Header("Audio")]
    public AudioSource[] audioSources;
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    [Header("Step Timing")]
    public float stepDelay = 0.5f;
    private float stepTimer = 0.5f;

    [Header("Dependencies")]
    //public CharacterController controller;

    private Vector3 lastPosition;
    bool isMoving = false;

    void Start()
    {
        

        if (audioSources == null || audioSources.Length == 0)
        {
            Debug.LogWarning("FootSteps_AudioManager: No audio sources assigned!");
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        // Detect movement
        isMoving = Vector3.Distance(currentPosition, lastPosition) > 0.01f;
        //if (!isMoving)
            
        if (isMoving||AlwaysTrue)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepDelay)
            {
                PlayRandomFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepDelay+1f;
        }

        lastPosition = currentPosition;
    }

    void PlayRandomFootstep()
    {
        if (audioSources == null || audioSources.Length == 0)
            return;

        int index = Random.Range(0, audioSources.Length);
        AudioSource source = audioSources[index];

        source.pitch = Random.Range(pitchRange.x, pitchRange.y);
        source.Play();
    }
}
