using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public bool isAtLady = false;
    [SerializeField] private GameObject player;
    //private float raycastDistance = 1f;

    //public Transform playerTransform; // Assign in the inspector
    //public Transform targetTransform;
    //public float proximityThreshold = 1f; // Define proximity distance


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (playerTransform == null || targetTransform == null) return; // Check for valid transforms

        //float distance = Vector3.Distance(playerTransform.position, targetTransform.position);

        //if (distance <= proximityThreshold)
        //{
        //    // Player is close enough to the target object
        //    Debug.Log("Player is at Lady Filigree");
        //    // Perform actions based on proximity (e.g., interact with object, trigger event)
        //    isAtLady = true;
        //}
        //else
        //{
        //    // Player is too far from the target object
        //    //Debug.Log("Player is currently not close to Lady Filigree");
        //    isAtLady = false;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAtLady = true;
        }
    }
}
