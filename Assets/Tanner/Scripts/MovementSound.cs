
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float pitchChange = 0.2f; //change pitch of steps
    public float stepInterval = 0.5f; // time between footsteps

    private AudioSource audioSource;
    private FPSController fpscontrollerScript;
    private float stepTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //get audio source on player
        fpscontrollerScript = GetComponent<FPSController>(); //get script
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isMoving && fpscontrollerScript.canMove)
        {
            stepTimer -= Time.deltaTime;

            if (!audioSource.isPlaying && stepTimer <= 0f && footstepSounds.Length > 0)
            {
                AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)]; //random sound from aray
                audioSource.pitch = 1 + Random.Range(-pitchChange, pitchChange); //random pitch of sound
                audioSource.PlayOneShot(clip); //play the clip once 
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer if not moving
        }
    }
}
