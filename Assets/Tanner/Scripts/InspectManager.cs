using UnityEngine;
using TMPro;

public class InspectManager : MonoBehaviour
{
    private Vector3 originalScale; // To store the original scale of the model

    [SerializeField] public GameObject self;
    public Vector2 originalSize;                        // Original size of the inventory UI (used to restore after resizing)

    public GameObject crosshairCanvas;

    public GameObject currentObservable;
    private RectTransform hideInv;                       // Reference to RectTransform of the inventory panel


    public bool rotateNow = false;
    public bool invResized = false;
    private FPSController fpscontrollerScript;          // Reference to the player�s movement controller
    public GameObject journalOverlay;
    public GameObject inspectText;
    [SerializeField] private GameObject inventory;      // Reference to the inventory UI GameObject
    public InventoryManager inventoryManager;
    public PlacementManager po;

    public GameObject obs;


    public void Start()
    {
        fpscontrollerScript = InventoryManager.Instance.player.GetComponent<FPSController>();       // Get the FPSController script from the player
        obs.SetActive(false);
        hideInv = InventoryManager.Instance.inventory.GetComponent<RectTransform>();                // Get RectTransform of the inventory panel for resizing
        originalSize = hideInv.sizeDelta;                                                           // Store original size of inventory panel
        inventoryManager = InventoryManager.Instance;                                               // Cache reference to the InventoryManager
        journalOverlay.SetActive(false);
        inspectText.SetActive(false);
    }
    public void SetCurrentObservable(GameObject inspectThis)
    {
        currentObservable = inspectThis;
        Debug.Log("current observable was set");
    }

    public void Update()
    {
        Debug.Log("current observable is " + currentObservable);
    }

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

    public void InspectItem()
    {
        currentObservable.SetActive(true);
        InventoryManager.Instance.obscamera.gameObject.SetActive(true);     // Enable the observation camera
        InventoryManager.currentlyInspecting = true;
        rotateNow = true;

        Debug.Log("inspect item is triggering and currently inspecting is currently " + InventoryManager.currentlyInspecting);
        if (InventoryManager.currentlyInspecting == true && currentObservable != null)
        {
            ResizeInvCanvas();
            fpscontrollerScript.canMove = false;    // stop player movement
            FPSController.canPickUp = false;
            journalOverlay.SetActive(true);

            Debug.Log("current observable LOLLLLLLLLLL name is " + currentObservable);

            if (currentObservable.name == "pivot1")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Solid Gold Casket";
                Debug.Log("it's running the currentObservable if");
            }
            else if (currentObservable.name == "pivot2")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Modern Casket";
            }
            else if (currentObservable.name == "pivot3")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Recycled Coffin";
            }
            else if (currentObservable.name == "pivot4")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Fern Bouquet";
            }
            else if (currentObservable.name == "pivot5")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Rose Bouquet";
            }
            else if (currentObservable.name == "pivot6")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Tulip Bouquet";
            }
            else if (currentObservable.name == "pivot7")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Orchid Bouquet";
            } else if (currentObservable.name == "pivot8")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Portrait of Lady";
            } else if (currentObservable.name == "pivot9")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Portrait of Child";
            } else if (currentObservable.name == "pivot10")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Portrait of King";
            } else if (currentObservable.name == "pivot11")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Broken Filigree Crest";
            } else if (currentObservable.name == "pivot12")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Box of Bugs";
            } else if (currentObservable.name == "pivot13")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Woven Shawl";
            } else if (currentObservable.name == "pivot14")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Half-Knit Quilt";
            } else if (currentObservable.name == "pivot15")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Filigree Keep Ledger";
            } else if (currentObservable.name == "pivot16")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "Stripped Goblet";
            } else if (currentObservable.name == "pivot20")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = "His Majesty";
            }



            inspectText.SetActive(true);

            if (inspectText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on this GameObject!");
                return; // Prevent NullReferenceException
            }


            //        Debug.Log("Clicked item: " + item.itemName + " (ID: " + item.id + ")");
            inventory.SetActive(false);
            inventoryManager.crosshairCanvas.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;


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
        inspectText.SetActive(false);
        InventoryManager.Instance.obscamera.Close();
        InventoryManager.Instance.obscamera.gameObject.SetActive(false);     // Enable the observation camera
                                                                             // Close observation camera
                                                                             //Debug.Log("registering exit clickobs");

        ResizeInvCanvas();                                                                      // Restore inventory canvas size
        InventoryManager.currentlyInspecting = false;
        //return;

        inventoryManager.placeObjects.canPlace = true;
        po.canPlace = true;                                       // Re-enable placement functionality

        inventory.SetActive(true);                                                              // Reactivate inventory UI and refresh list
        inventoryManager.ListItems();
        //inventoryManager.ToggleCursor();

        fpscontrollerScript.canMove = false;                                                    // Lock player movement during inventory

        FPSController.canPickUp = false;                                                        // Prevent picking up objects while inventory is open

    }

}
