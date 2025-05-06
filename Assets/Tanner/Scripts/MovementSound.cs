//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MovementSound : MonoBehaviour
//{
//    // Start is called before the first frame update
//    public AudioClip[] footstepSounds;
//    public float pitchChange = 0.2f;
//    private AudioSource audioSource;
//    private FPSController fpscontrollerScript;
//    public bool canPlayWalkingSound;
//    void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//        fpscontrollerScript = GetComponent<FPSController>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) && canPlayWalkingSound == false)
//        {
//            if (fpscontrollerScript.canMove == true)
//            {
//                canPlayWalkingSound = true;
//                audioSource.mute = false;
//            }
//        }
//        else
//        {
//            canPlayWalkingSound = false;
//            audioSource.mute = true;
//        }

//        if (canPlayWalkingSound)
//        {

//            if (footstepSounds.Length > 0)
//            {
//                int randomIndex = Random.Range(0, footstepSounds.Length);
//                audioSource.clip = footstepSounds[randomIndex];
//                audioSource.pitch = 1 + Random.Range(-pitchChange, pitchChange);
//                audioSource.Play();
//            }
//        }

//    }


//}



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
