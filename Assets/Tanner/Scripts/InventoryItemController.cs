using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;                                   // Reference to the actual item this controller represents
    public InventoryManager inventoryManager;           // Reference to the InventoryManager

    public DialogueUI DialogueManager;                  // Reference to a Dialogue UI (not actively used in this script)

    public PlaceObjects placeObjects;                   // Reference to the PlaceObjects script (handles placing objects in world)

    public float sensitivity = 10f;                     // Sensitivity for rotating objects during inspection

    public bool checkthis = false;                      // Flags used for internal checks
    public bool invResized = false;

    public GameObject placeobj;                         // Placeholder for an object that might be placed

    private FPSController fpscontrollerScript;          // Reference to the player�s movement controller



    public float objectRotationSpeed = 5f;              // Rotation speed for inspection

    public float deltaRotationX;                        // Mouse delta rotation values
    public float deltaRotationY;

    public GameObject currentObservable;                // Reference to the currently observable object being inspected
    public Vector2 originalSize;                        // Original size of the inventory UI (used to restore after resizing)

    public RectTransform hideInv;                       // Reference to RectTransform of the inventory panel
    [SerializeField] private GameObject inventory;      // Reference to the inventory UI GameObject


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
    }

    void Update()
    {
        // If left mouse button is held down while inspecting, rotate object
       // if (Input.GetMouseButton(0))
        //{
            InspectingWithMouse();
            //Debug.Log("it should be calling inspecting with mouse rn");
       // }

        // If pressing Tab while inspecting, exit inspection mode
        if (Input.GetKeyDown(KeyCode.Tab) && InventoryManager.currentlyInspecting == true)
        {
            //set canvas size back to normal 

            // currentObservable.SetActive(false);                                                     // Hide the currently observable object 
            fpscontrollerScript.canMove = true;                                                     // Allow player movement again


            InventoryManager.Instance.obscamera.Close();                                            // Close observation camera
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

        //        Debug.Log("Clicked item: " + item.itemName + " (ID: " + item.id + ")");
        inventory.SetActive(false);
        inventoryManager.crosshairCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        InventoryManager.Instance.obscamera.gameObject.SetActive(true);     // Enable the observation camera

        // Activate the corresponding observable object based on item ID
        if (item.id == 1)
        {
            InventoryManager.Instance.ObservableObject1.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject1;
            InventoryManager.currentlyInspecting = true;
            Debug.Log(currentObservable);
        }

        else if (item.id == 2)
        {
            InventoryManager.Instance.ObservableObject2.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject2;

        }

        else if (item.id == 3)
        {
            InventoryManager.Instance.ObservableObject3.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject3;
        }

        else if (item.id == 4)
        {
            InventoryManager.Instance.ObservableObject4.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject4;
        }

        else if (item.id == 5)
        {
            InventoryManager.Instance.ObservableObject5.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject5;
        }

        else if (item.id == 6)
        {
            InventoryManager.Instance.ObservableObject6.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject6;
        }

        else if (item.id == 7)
        {
            InventoryManager.Instance.ObservableObject7.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject7;
        }

        else if (item.id == 8)
        {
            InventoryManager.Instance.ObservableObject8.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject8;
            Debug.LogError("currentObservable is NULL on " + gameObject.name);
            return;
        }

        else if (item.id == 9)
        {
            InventoryManager.Instance.ObservableObject9.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject9;
            Debug.LogError("currentObservable is NULL on " + gameObject.name);
            return;
        }
        else
        {
            currentObservable = InventoryManager.Instance.ObservableObject1;
        }

    }


    // Rotate the inspected object based on mouse movement
    public void InspectingWithMouse()
    {
        Debug.Log("inspecting currently is" + InventoryManager.currentlyInspecting);
        InventoryManager.currentlyInspecting = true;

        //Debug.Log("Inspectingwithmouse");
        if (InventoryManager.currentlyInspecting == true)
        {
            Debug.Log(sensitivity);
            deltaRotationX = Input.GetAxisRaw("Mouse X") * sensitivity;           // Get mouse movement delta
            deltaRotationY = Input.GetAxisRaw("Mouse Y") * sensitivity;


            Debug.Log("registering inspect rotation and rotation x and y are " + deltaRotationX.ToString() + " " + deltaRotationY.ToString());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (deltaRotationX != 0 || deltaRotationY != 0)
            {

                // Rotate the object in world space
                currentObservable.transform.Rotate(Vector3.up, deltaRotationX, Space.World);
                currentObservable.transform.Rotate(Vector3.right, -deltaRotationY, Space.World);

                // //previous version:
                // Quaternion rotationY = Quaternion.AngleAxis(deltaRotationY * objectRotationSpeed, Vector3.right); // Rotate around Y-axis (horizontal)
                // Quaternion rotationX = Quaternion.AngleAxis(deltaRotationX * objectRotationSpeed, Vector3.up); // Rotate around X-axis (vertical)
                // // Apply rotation to the object
                // currentObservable.transform.rotation = rotationX * rotationY * currentObservable.transform.rotation;

                Debug.Log(deltaRotationY);

                //another try that didn't work:
                //currentObservable.transform.Rotate(deltaRotationX * Vector3.right * objectRotationSpeed * Time.deltaTime 
                //   + deltaRotationY * Vector3.up * objectRotationSpeed * Time.deltaTime 
                //  + Vector3.forward * objectRotationSpeed * Time.deltaTime, Space.Self);

            }
        }
    }


    // Resize or restore the inventory canvas to hide/show
    public void ResizeInvCanvas()
    {
        //get the canvas ui object from parent and set the width and height to 0, this will hide it without removing the button with code

        if (invResized == false)
        {
            hideInv.sizeDelta = new Vector2(0, 0);
            invResized = true;
        }
        else
        {
            hideInv.sizeDelta = originalSize;
        }

    }
}





