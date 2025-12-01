using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;                           // The Item data (scriptable object) this pickup represents

    public GameObject player;                   // Reference to the player
    private FPSController fpscontrollerScript;  // To check movement/pickup states
    public Camera camera;                       // Camera used to raycast
    private RaycastHit hit1;                     // Stores info about raycast hits

    public GameObject defaultCrossHair;
    public GameObject pickUpCrosshair;

    public bool currentlyPickingUp = false;
    private RaycastHit hit;



    // These are not needed
    //private PlaceObjects placeobjects;
    //public GameObject cameraobj;
    private void Awake()
    {

        fpscontrollerScript = player.GetComponent<FPSController>(); // Get the FPSController on the player
        camera = Camera.main;                                       // Use the main camera for raycasting

        //placeobjects = cameraobj.GetComponent<PlaceObjects>();

        pickUpCrosshair.SetActive(false);
    }

    // Handles picking up the item
    void Pickup()
    {
        currentlyPickingUp = true;
        InventoryManager.Instance.playpickupsound();    // Play pickup sound effect through InventoryManager
        InventoryManager.Instance.Add(Item);            // Add item to the InventoryManager list
        Destroy(gameObject);                            // Destroy the pickup object in the scene
    }
    private void Update()
    {
        //Debug.Log("currentlyPickingUp is" + currentlyPickingUp);
        //Vector3 origin = camera.transform.position;
        //Vector3 direction

        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
        {
            if (hit.collider.CompareTag("InteractiveObject"))
            {
                //Debug.Log("triggering crosshair grabby hand");
                pickUpCrosshair.SetActive(true);
                defaultCrossHair.SetActive(false);
            }
        }
        else
        {
            pickUpCrosshair.SetActive(false);
            defaultCrossHair.SetActive(true);
        }
    }

    // Called by Unity when the object is clicked with the mouse
    private void OnMouseDown()
    {
        // Only allow pickup if FPSController says we can
        if (FPSController.canPickUp == true)
        {
            // Perform a raycast from the camera forward to check if we are looking at this item
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit1, Mathf.Max(5)))
            {
                pickUpCrosshair.SetActive(true);
                defaultCrossHair.SetActive(false);
                Pickup();   // If ray hits, pick up the item
            }

        }
    }

    private void OnMouseUpAsButton()
    {
        currentlyPickingUp = false;
        pickUpCrosshair.SetActive(false);
        defaultCrossHair.SetActive(true);
    }
}
