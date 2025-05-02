using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] footstepSounds;
    public float pitchChange = 0.2f;
    private AudioSource audioSource;
    private FPSController fpscontrollerScript;
    private bool canPlayWalkingSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fpscontrollerScript = GetComponent<FPSController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) )
        {
            if (fpscontrollerScript.canMove == true)
            {
                canPlayWalkingSound = true;
                
            }
        }
        else
        {
            canPlayWalkingSound = false;
        }
        if (canPlayWalkingSound)
        {
            
            if (footstepSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, footstepSounds.Length);
                audioSource.clip = footstepSounds[randomIndex];
                audioSource.pitch = 1 + Random.Range(-pitchChange, pitchChange);
                audioSource.Play();
            }
        }
    }

    //private void PlayFootstepSound()
    //{
    //    if (footstepSounds.Length > 0)
    //    {
    //        int randomIndex = Random.Range(0, footstepSounds.Length);
    //        audioSource.clip = footstepSounds[randomIndex];
    //        audioSource.pitch = 1 + Random.Range(-pitchChange, pitchChange);
    //        audioSource.Play();
            
    //    }
    //}
}
