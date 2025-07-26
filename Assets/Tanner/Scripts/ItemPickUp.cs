using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
	public Item Item;                           // The Item data (scriptable object) this pickup represents
                                                
    public GameObject player;                   // Reference to the player
    private FPSController fpscontrollerScript;  // To check movement/pickup states
    public Camera camera;                       // Camera used to raycast
    private RaycastHit hit;                     // Stores info about raycast hits



    // These are not needed
    //private PlaceObjects placeobjects;
    //public GameObject cameraobj;
    private void Awake()
    {
        
        fpscontrollerScript = player.GetComponent<FPSController>(); // Get the FPSController on the player
        camera = Camera.main;                                       // Use the main camera for raycasting

        //placeobjects = cameraobj.GetComponent<PlaceObjects>();
    }

    // Handles picking up the item
    void Pickup()
	{
        InventoryManager.Instance.playpickupsound();    // Play pickup sound effect through InventoryManager
        InventoryManager.Instance.Add(Item);            // Add item to the InventoryManager list
        Destroy(gameObject);                            // Destroy the pickup object in the scene

    }
    private void Update()
    {
        //Vector3 origin = camera.transform.position;
        //Vector3 direction
    }

    // Called by Unity when the object is clicked with the mouse
    private void OnMouseDown()
	{
        // Only allow pickup if FPSController says we can
        if (FPSController.canPickUp == true)
		{
           
            // Perform a raycast from the camera forward to check if we are looking at this item
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
			{
                Pickup();   // If ray hits, pick up the item
            }
			
		}
	}

}
