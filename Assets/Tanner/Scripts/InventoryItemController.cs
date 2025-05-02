using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public InventoryManager inventoryManager;

    public DialogueUI DialogueManager;

    public PlaceObjects placeObjects;

    public float sensitivity = 10f;
    public bool checkthis = false;
    public bool invResized = false;
    public GameObject placeobj;

    private FPSController fpscontrollerScript;

    public bool currentlyInspecting = false;

    public float objectRotationSpeed = 5f;

    public float deltaRotationX;
    public float deltaRotationY;

    public GameObject currentObservable;
    public Vector2 originalSize;

    public RectTransform hideInv;
    [SerializeField] private GameObject inventory;


    //this is started when inventory is opened on each inventory button




    void Awake()
    {


    }

    void Start()
    {
        fpscontrollerScript = InventoryManager.Instance.player.GetComponent<FPSController>(); //call other script
        hideInv = InventoryManager.Instance.inventory.GetComponent<RectTransform>();
        originalSize = hideInv.sizeDelta;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            InspectingWithMouse();
        }

        if (Input.GetKeyDown(KeyCode.X) && currentlyInspecting == true)
        {
            //set canvas size back to normal 
            currentObservable.SetActive(false);
            currentlyInspecting = false;
            fpscontrollerScript.canMove = true;
            FPSController.canPickUp = true;
            InventoryManager.Instance.obscamera.Close();
            Debug.Log("registering exit clickobs");
            ResizeInvCanvas();
            return;
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;






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
        InventoryManager.Instance.Remove(item);



        CloseInventory();




        Destroy(gameObject);
    }



    public void UseItem()
    {

        if (item == null)
        {
            Debug.LogWarning("Item is null in UseItem!");

        }



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
        RemoveItem();

    }

    void CloseInventory()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.CloseInventoryButton();
            InventoryManager.Instance.TurnoffInv(); // optional if you need that too
        }
    }


    public void InspectItem()
    {
        ResizeInvCanvas();
        fpscontrollerScript.canMove = false;
        currentlyInspecting = true;
        FPSController.canPickUp = false;
        Debug.Log("Clicked item: " + item.itemName + " (ID: " + item.id + ")");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InventoryManager.Instance.obscamera.gameObject.SetActive(true);

        if (item.id == 1)
        {
            InventoryManager.Instance.ObservableObject1.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject1;
        }

        if (item.id == 2)
        {
            InventoryManager.Instance.ObservableObject2.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject2;

        }

        if (item.id == 3)
        {
            InventoryManager.Instance.ObservableObject3.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject3;
        }

        if (item.id == 4)
        {
            InventoryManager.Instance.ObservableObject4.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject4;
        }

        if (item.id == 5)
        {
            InventoryManager.Instance.ObservableObject5.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject5;
        }

        if (item.id == 6)
        {
            InventoryManager.Instance.ObservableObject6.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject6;
        }

        if (item.id == 7)
        {
            InventoryManager.Instance.ObservableObject7.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject7;
        }

        if (item.id == 8)
        {
            InventoryManager.Instance.ObservableObject8.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject8;
        }

        if (item.id == 9)
        {
            InventoryManager.Instance.ObservableObject9.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject9;
        }

    }

    public void InspectingWithMouse()
    {
        //Debug.Log("Inspectingwithmouse");
        if (currentlyInspecting == true)
        {
            deltaRotationX = -Input.GetAxis("Mouse X") * sensitivity;
            deltaRotationY = -Input.GetAxis("Mouse Y") * sensitivity;


            Debug.Log("registering inspect rotation");

            if (deltaRotationX != 0 && deltaRotationY != 0)
            {
                
                currentObservable.transform.Rotate(Vector3.up, deltaRotationX, Space.World);
                currentObservable.transform.Rotate(Vector3.right, -deltaRotationY,  Space.World);
                
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





