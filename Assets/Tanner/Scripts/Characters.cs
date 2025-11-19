using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{

    public GameObject canvasCrosshair;
    public GameObject defaultCrosshair;

    public GameObject speechCrosshair;
    public bool isAtCharacter = false;                   // Flag to indicate if the player is currently in range of this character (the lady)

    public static bool isAtGardener = false;
    public Sprite speechTexture;

    public static bool isAtLady = false;
    [SerializeField] private GameObject player;     // Reference to player (not used in final logic here, but serialized for flexibility)
    //private float raycastDistance = 1f;
    public Sprite crosshairMouse;

    //public Transform playerTransform; // Assign in the inspector
    //public Transform targetTransform;
    //public float proximityThreshold = 1f; // Define proximity distance


    // Start is called before the first frame update
    void Start()
    {
        defaultCrosshair.SetActive(true);
        speechCrosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(crosshairMouse);
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
        speechCrosshair.SetActive(true);
        defaultCrosshair.SetActive(false);
        // If the object entering has the Player tag, mark that were at the lady
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.name.ToString() == "LadyFiligree")
            {
                isAtLady = true;
                canvasCrosshair.SetActive(false);
                Debug.Log("is now at lady?" + isAtLady);
            }
            else if (this.gameObject.name.ToString() == "Gardener")
            {
                isAtGardener = true;
               canvasCrosshair.SetActive(false);
                Debug.Log("is now at gardener?" + isAtGardener);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        speechCrosshair.SetActive(false);
        defaultCrosshair.SetActive(true);
        // If the object entering has the Player tag, mark that were at the lady

    }

}
