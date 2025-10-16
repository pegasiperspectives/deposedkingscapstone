using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public bool isAtCharacter = false;                   // Flag to indicate if the player is currently in range of this character (the lady)

    public bool isAtGardener = false;

    public bool isAtLady = false;
    [SerializeField] private GameObject player;     // Reference to player (not used in final logic here, but serialized for flexibility)
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

    // When something enters this character�s trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the object entering has the Player tag, mark that we�re at the lady
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.name.ToString() == "LadyFiligree")
            {
                isAtLady = true;
                Debug.Log("is now at lady?" + isAtLady);
            } else if (this.gameObject.name.ToString() == "Gardener")
            {
                isAtGardener = true; 
                Debug.Log("is now at gardener?" + isAtGardener);
            }
            
        }
    }
}
