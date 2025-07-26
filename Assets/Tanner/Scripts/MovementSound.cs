
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public AudioClip[] footstepSounds;  // Array of footstep sounds to randomly choose from
    public float pitchChange = 0.2f;    // change pitch of steps
    public float stepInterval = 0.5f;   // time between footsteps

    // Internal references
    private AudioSource audioSource;            // Audio source on player
    private FPSController fpscontrollerScript;  // To check if player can move
    private float stepTimer = 0f;               // Countdown until next step sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource on the same GameObject (player)
        fpscontrollerScript = GetComponent<FPSController>(); // Get FPSController script on the same GameObject to check movement state
    }

    void Update()
    {
        // Check if movement keys are pressed
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Only play footsteps if player is moving and movement is allowed
        if (isMoving && fpscontrollerScript.canMove)
        {
            stepTimer -= Time.deltaTime;    // Count down the timer

            // When timer reaches zero, play a footstep sound
            if (!audioSource.isPlaying && stepTimer <= 0f && footstepSounds.Length > 0)
            {
                AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];    // Pick a random footstep sound from the array
                audioSource.pitch = 1 + Random.Range(-pitchChange, pitchChange);            // Slightly randomize pitch for variation

                audioSource.PlayOneShot(clip);  // Play sound once without interrupting other sounds 
                stepTimer = stepInterval;       // Reset timer until next step
            }
        }
        else
        {
            stepTimer = 0f; // If not moving, reset timer so next movement plays immediately
        }
    }
}
