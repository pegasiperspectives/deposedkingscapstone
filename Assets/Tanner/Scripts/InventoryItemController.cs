using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryItemController : MonoBehaviour
{
    public Item item;                                   // Reference to the actual item this controller represents
    public InventoryManager inventoryManager;           // Reference to the InventoryManager

    public DialogueUI DialogueManager;                  // Reference to a Dialogue UI (not actively used in this script)

    public PlaceObjects placeObjects;                   // Reference to the PlaceObjects script (handles placing objects in world)
    private Vector3 lastMousePos;

    public float sensitivity = 10f;                     // Sensitivity for rotating objects during inspection

    public bool checkthis = false;                      // Flags used for internal checks
    public bool invResized = false;

    public GameObject placeobj;                         // Placeholder for an object that might be placed

    private FPSController fpscontrollerScript;          // Reference to the player�s movement controller




    public float deltaRotationX;                        // Mouse delta rotation values
    public float deltaRotationY;

    public static GameObject currentObservable;                // Reference to the currently observable object being inspected
    public Vector2 originalSize;                        // Original size of the inventory UI (used to restore after resizing)

    private RectTransform hideInv;                       // Reference to RectTransform of the inventory panel
    [SerializeField] private GameObject inventory;      // Reference to the inventory UI GameObject

    [SerializeField] private InputAction pressed, axis;
    public bool rotateNow = false;
    public Vector2 rotation;
    public GameObject journalOverlay;

    //this is started when inventory is opened on each inventory button




    void Awake()
    {

    }


    void Start()
    {
        fpscontrollerScript = InventoryManager.Instance.player.GetComponent<FPSController>();       // Get the FPSController script from the player
        hideInv = InventoryManager.Instance.inventory.GetComponent<RectTransform>();                // Get RectTransform of the inventory panel for resizing
        originalSize = hideInv.sizeDelta;                                                           // Store original size of inventory panel
        inventoryManager = InventoryManager.Instance;                                               // Cache reference to the InventoryManager
        journalOverlay.SetActive(false);

        InventoryManager.Instance.ObservableObject1.SetActive(false);
        InventoryManager.Instance.ObservableObject2.SetActive(false);
        InventoryManager.Instance.ObservableObject3.SetActive(false);
        InventoryManager.Instance.ObservableObject4.SetActive(false);
        InventoryManager.Instance.ObservableObject5.SetActive(false);
        InventoryManager.Instance.ObservableObject6.SetActive(false);
        InventoryManager.Instance.ObservableObject7.SetActive(false);
        InventoryManager.Instance.ObservableObject8.SetActive(false);
        InventoryManager.Instance.ObservableObject9.SetActive(false);


    }

    void Update()
    {

    }



    // Assign an item to this slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        // If placeObjects isn�t set, try to find it from player�s camera
        if (placeObjects == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                Transform camTransform = player.transform.Find("Camera");
                if (camTransform != null)
                {
                    placeObjects = camTransform.GetComponent<PlaceObjects>();
                }
            }
        }






    }
    public void RemoveItem() //removing from inventory list; not to be accessed again
    {
        InventoryManager.Instance.Remove(item);     // Remove from manager

        CloseInventory();                           // Close inventory UI

        //gameObject.SetActive(false);                        // Destroy this item button
    }


    // Use the item and trigger placement flags
    public void UseItem()
    {

        if (item == null)
        {
            Debug.LogWarning("Item is null in UseItem!");

        }


        // Set a flag in PlaceObjects depending on item ID
        if (item.id == 1)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsExample1 = true;
        }
        else if (item.id == 2)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsExample2 = true;
        }
        else if (item.id == 3)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsExample3 = true;
        }
        else if (item.id == 4)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsFern = true;
        }
        else if (item.id == 5)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsRoses = true;
        }
        else if (item.id == 6)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsTulips = true;
        }
        else if (item.id == 7)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsOrchids = true;
        }
        else if (item.id == 8)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsLadyPort = true;
        }
        else if (item.id == 9)
        {
            var placer = placeObjects;
            PlaceObjects.placeIsChildPort = true;
        }
        RemoveItem();   // Remove item after using

    }


    // Close inventory UI safely
    void CloseInventory()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.CloseInventoryButton();
            InventoryManager.Instance.TurnoffInv(); // optional if you need that too
        }
    }

    // Start inspecting an item
    public void InspectItem()
    {
        ResizeInvCanvas();
        fpscontrollerScript.canMove = false;    // stop player movement
        FPSController.canPickUp = false;
        journalOverlay.SetActive(true);


        //        Debug.Log("Clicked item: " + item.itemName + " (ID: " + item.id + ")");
        inventory.SetActive(false);
        inventoryManager.crosshairCanvas.SetActive(false);
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;


        InventoryManager.Instance.obscamera.gameObject.SetActive(true);     // Enable the observation camera

        // Activate the corresponding observable object based on item ID
        if (item.id == 1) //gold - works but now turns into the recycled coffin
        {
            InventoryManager.Instance.ObservableObject1.SetActive(true);

            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);
            InventoryManager.Instance.ObservableObject9.SetActive(false);

            currentObservable = InventoryManager.Instance.ObservableObject1;
            InventoryManager.currentlyInspecting = true;
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
            // currentObservable.SetActive(true);

        }

        else if (item.id == 2) //modern - goes to orchids for some reason
        {
            InventoryManager.Instance.ObservableObject2.SetActive(true);

            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);
            InventoryManager.Instance.ObservableObject9.SetActive(false);

            currentObservable = InventoryManager.Instance.ObservableObject2;
            InventoryManager.currentlyInspecting = true;
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
            // currentObservable.SetActive(true);
        }

        else if (item.id == 3) //recycled - goes to fern for some reason
        {
            InventoryManager.Instance.ObservableObject3.SetActive(true);

            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);
            InventoryManager.Instance.ObservableObject9.SetActive(false);
            InventoryManager.currentlyInspecting = true;

            currentObservable = InventoryManager.Instance.ObservableObject3;
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
        }

        else if (item.id == 4) //fern - works
        {
            InventoryManager.Instance.ObservableObject4.SetActive(true);

            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);
            InventoryManager.Instance.ObservableObject9.SetActive(false);

            InventoryManager.currentlyInspecting = true;


            currentObservable = InventoryManager.Instance.ObservableObject4;
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
        }

        else if (item.id == 5) //roses - works
        {
            InventoryManager.Instance.ObservableObject5.SetActive(true);

            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);
            InventoryManager.Instance.ObservableObject9.SetActive(false);

            InventoryManager.currentlyInspecting = true;

            currentObservable = InventoryManager.Instance.ObservableObject5;
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
        }

        else if (item.id == 6) //tulips - works
        {
            InventoryManager.Instance.ObservableObject6.SetActive(true);

            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);
            InventoryManager.Instance.ObservableObject9.SetActive(false);

            currentObservable = InventoryManager.Instance.ObservableObject6;
            InventoryManager.currentlyInspecting = true;
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
        }

        else if (item.id == 7) //orchids - won't spin
        {
            InventoryManager.Instance.ObservableObject7.SetActive(true);

            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);
            InventoryManager.Instance.ObservableObject9.SetActive(false);

            InventoryManager.currentlyInspecting = true;
            currentObservable = InventoryManager.Instance.ObservableObject7;
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
        }

        else if (item.id == 8)
        {
            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);

            InventoryManager.Instance.ObservableObject8.SetActive(true);

            InventoryManager.Instance.ObservableObject9.SetActive(false);
            InventoryManager.currentlyInspecting = true;

            currentObservable = InventoryManager.Instance.ObservableObject8;
            Debug.LogError("currentObservable is NULL on " + gameObject.name);
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
            return;
        }

        else if (item.id == 9)
        {
            InventoryManager.Instance.ObservableObject4.SetActive(false);
            InventoryManager.Instance.ObservableObject2.SetActive(false);
            InventoryManager.Instance.ObservableObject1.SetActive(false);
            InventoryManager.Instance.ObservableObject3.SetActive(false);
            InventoryManager.Instance.ObservableObject5.SetActive(false);
            InventoryManager.Instance.ObservableObject6.SetActive(false);
            InventoryManager.Instance.ObservableObject7.SetActive(false);
            InventoryManager.Instance.ObservableObject8.SetActive(false);

            InventoryManager.Instance.ObservableObject9.SetActive(true);
            InventoryManager.currentlyInspecting = true;

            currentObservable = InventoryManager.Instance.ObservableObject9;
            Debug.LogError("currentObservable is NULL on " + gameObject.name);
            Debug.Log("object 1 is " + currentObservable + "and is active");
            rotateNow = true;
            return;
        }
        else
        {
            Debug.Log("hi currentobservable is false now and so is rotate now yay");
            currentObservable = null;
            //currentObservable = InventoryManager.Instance.ObservableObject1;
            rotateNow = false;
        }

    }


    // Resize or restore the inventory canvas to hide/show
    public void ResizeInvCanvas()
    {
        //get the canvas ui object from parent and set the width and height to 0, this will hide it without removing the button with code

        if (invResized == false)
        {
            //            hideInv.sizeDelta = new Vector2(0, 0);
            invResized = true;
        }
        else
        {
            hideInv.sizeDelta = originalSize;
        }

    }

    public void CloseInspect()
    {
        Debug.Log("it's trying to close the inspect journal");
        //set canvas size back to normal 

        inventoryManager.crosshairCanvas.SetActive(true);

        InventoryManager.Instance.ObservableObject1.SetActive(false);
        InventoryManager.Instance.ObservableObject2.SetActive(false);
        InventoryManager.Instance.ObservableObject3.SetActive(false);
        InventoryManager.Instance.ObservableObject4.SetActive(false);
        InventoryManager.Instance.ObservableObject5.SetActive(false);
        InventoryManager.Instance.ObservableObject6.SetActive(false);
        InventoryManager.Instance.ObservableObject7.SetActive(false);
        InventoryManager.Instance.ObservableObject8.SetActive(false);
        InventoryManager.Instance.ObservableObject9.SetActive(false);

        currentObservable = null;

        //currentObservable.SetActive(false);                                                     // Hide the currently observable object 
        fpscontrollerScript.canMove = true;                                                     // Allow player movement again

        rotateNow = false;

        journalOverlay.SetActive(false);
        InventoryManager.Instance.obscamera.Close();
        InventoryManager.Instance.obscamera.gameObject.SetActive(false);     // Enable the observation camera
                                                                             // Close observation camera
                                                                             //Debug.Log("registering exit clickobs");

        ResizeInvCanvas();                                                                      // Restore inventory canvas size
        InventoryManager.currentlyInspecting = false;
        //return;

        inventoryManager.placeObjects.canPlace = true;                                          // Re-enable placement functionality

        inventory.SetActive(true);                                                              // Reactivate inventory UI and refresh list
        inventoryManager.ListItems();
        //inventoryManager.ToggleCursor();

        fpscontrollerScript.canMove = false;                                                    // Lock player movement during inventory

        FPSController.canPickUp = false;                                                        // Prevent picking up objects while inventory is open

    }

}






