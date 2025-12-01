using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{

    public GameObject canvasCrosshair;
    public GameObject defaultCrosshair;
    public GameObject grabby;

    public GameObject speechCrosshair;
    public bool isAtCharacter = false;                   // Flag to indicate if the player is currently in range of this character (the lady)

    public static bool isAtGardener = false;

    public static bool isAtLady = false;
    [SerializeField] private GameObject player;     // Reference to player (not used in final logic here, but serialized for flexibility)

    public DialogueUI dialogueManager;
    //private float raycastDistance = 1f;

    public Camera camera;
    private RaycastHit hit;
    [SerializeField] public InventoryManager inventory;  // Inventory UI (to check if open)

    private FPSController fpscontrollerScript;      // Reference to player controller
    private int opened = 0;
    public GameObject speechBubbles;


    //public Transform playerTransform; // Assign in the inspector
    //public Transform targetTransform;
    //public float proximityThreshold = 1f; // Define proximity distance


    // Start is called before the first frame update
    void Start()
    {
        defaultCrosshair.SetActive(true);
        speechCrosshair.SetActive(false);
        camera = Camera.main;                                       // Use the main camera for raycasting
        fpscontrollerScript = player.GetComponent<FPSController>();

    }

    // Update is called once per frame
    void Update()
    {
        /*   if (inventory.inventory.activeInHierarchy == false && Input.GetKeyDown(KeyCode.Tab))
           {
               Debug.Log("should be opening inventory " + opened + " times");
               inventory.inventory.SetActive(true);
               opened++;
           }

           /* else if (inventory.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Tab))
           {
               Debug.Log("should be closing inventory");
               inventory.SetActive(false);
           } */

        if (dialogueManager.self.activeInHierarchy == true)
        {
            fpscontrollerScript.canMove = false;
        }

        //        Debug.Log(crosshairMouse);
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

        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
        {
            if (hit.collider.CompareTag("character"))
            {
                //Debug.Log("triggering speech bubble now yayy...");
                speechCrosshair.SetActive(true);
                defaultCrosshair.SetActive(false);

                if (speechCrosshair.activeInHierarchy == true && (isAtLady == true || isAtGardener == true) && Input.GetMouseButtonDown(0))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
        else
        {
            //Debug.Log("making speech cross hair go bye bye");
            speechCrosshair.SetActive(false);
            defaultCrosshair.SetActive(true);
            //speechBubbles.SetActive(false);
        }
    }

    // When something enters this character�s trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the object entering has the Player tag, mark that were at the lady
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.name.ToString() == "LadyFiligree")
            {
                isAtLady = true;
                Debug.Log("is now at lady?" + isAtLady);
            }
            else if (this.gameObject.name.ToString() == "Gardener")
            {
                isAtGardener = true;
                Debug.Log("is now at gardener?" + isAtGardener);
            }

        }
    }

  /*  private void OnTriggerExit(Collider other)
    {
        // If the object entering has the Player tag, mark that were at the lady
        if (other.CompareTag("Player"))
        {
            speechBubbles.SetActive(false);
        }
    } */


}
