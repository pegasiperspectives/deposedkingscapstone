using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public InventoryManager inventoryManager;

    public PlaceObjects placeObjects;
    public bool checkthis = false;
    public GameObject placeobj;

    private FPSController fpscontrollerScript;

    public bool currentlyInspecting = false;

    public float objectRotationSpeed = 100f;

    public float deltaRotationX;
    public float deltaRotationY;

    public GameObject currentObservable;

    [SerializeField] private GameObject inventory;

    //this is started when inventory is opened on each inventory button




    void Awake()
    {


    }

    void Start()
    {
        fpscontrollerScript = InventoryManager.Instance.player.GetComponent<FPSController>(); //call other script

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            InspectingWithMouse();
            
        }

        if (Input.GetKey(KeyCode.S) && currentlyInspecting == true)
        {
        //set canvas size back to normal 
            currentlyInspecting = false;
            fpscontrollerScript.canMove = true;
            FPSController.canPickUp = true;
            InventoryManager.Instance.obscamera.Close();
            Debug.Log("registering exit clickobs");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        //CloseInventory();

        //get the canvas ui object from parent and set the width and height to 0, this will hide it without removing the button with code

        
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
            InventoryManager.Instance.ObservableObject1.SetActive(true);
            currentObservable = InventoryManager.Instance.ObservableObject3;
        }

    }

    public void InspectingWithMouse()
    {
        //Debug.Log("Inspectingwithmouse");
        if (currentlyInspecting == true)
        {
            deltaRotationX = -Input.GetAxis("Mouse X");
            deltaRotationY = Input.GetAxis("Mouse Y");


            Debug.Log("registering inspect rotation");

            if (deltaRotationX != 0 && deltaRotationY != 0)
            {
                Quaternion rotationY = Quaternion.AngleAxis(deltaRotationY * objectRotationSpeed * Time.deltaTime, Vector3.right); // Rotate around Y-axis (horizontal)
                Quaternion rotationX = Quaternion.AngleAxis(deltaRotationX * objectRotationSpeed * Time.deltaTime, Vector3.up); // Rotate around X-axis (vertical)
                Debug.Log(deltaRotationY);

                // Apply rotation to the object
                //currentObservable.transform.rotation = rotationX * transform.rotation * rotationY;
                currentObservable.transform.rotation = rotationY * rotationX * currentObservable.transform.rotation;
            }
        }
    }
}





