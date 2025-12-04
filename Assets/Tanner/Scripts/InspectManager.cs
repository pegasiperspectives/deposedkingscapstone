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
    public GameObject inspectTextDescription;
    [SerializeField] private GameObject inventory;      // Reference to the inventory UI GameObject
    public InventoryManager inventoryManager;
    public PlacementManager po;

    public GameObject obs;

    #region Inspect Text Arrays - Topher Code
    // All possible inspect text that can be shown based on items

    [Header("Inspect Text Arrays")] // header to split the dialogue in the inspector
    // Separate item text arrays - organized in the same order as the Item Sheet google sheet / Inventory page
    [SerializeField] string[] memoText = { };                   //Memo
    [SerializeField] string[] solidGoldCasketText = { };        //Solid Gold Casket
    [SerializeField] string[] modernCasketText = { };           //Modern Casket
    [SerializeField] string[] recycledCoffinText = { };         //Recycled Coffin
    [SerializeField] string[] fernBoquetText = { };             //Fern Boquet
    [SerializeField] string[] roseBoquetText = { };             //Rose Boquet
    [SerializeField] string[] orchidBoquetText = { };           //Orchid Boquet
    [SerializeField] string[] tulipBoquetText = { };             //Tulip Boquet
    [SerializeField] string[] brokenFiligreeCrestText = { };    //Broken Filigree Crest
    [SerializeField] string[] boxofBugsText = { };              //Box of Bugs
    [SerializeField] string[] wovenShawlText = { };             //Woven Shawl
    [SerializeField] string[] halfKnitQuiltText = { };          //Half-Knit Quilt
    [SerializeField] string[] filigreeKeepLedgerText = { };     //Filigree Keep Ledger
    [SerializeField] string[] strippedGobletText = { };         //Stripped Goblet
    [SerializeField] string[] portraitofLadyText = { };         //Portrait of Lady
    [SerializeField] string[] portraitofKingText = { };         //Portrait of King
    [SerializeField] string[] portraitofChildText = { };        //Portrait of Child
    [SerializeField] string[] rustyKeyText = { };               //Rusty Key
    [SerializeField] string[] quartersKeyText = { };            //Quarter's Key
    [SerializeField] string[] woodenPlankText = { };            //Suspiciously Long Wooden Plank
    [SerializeField] string[] hisMajestyText = { };             //His Majesty


    #endregion

    public void Start()
    {
        fpscontrollerScript = InventoryManager.Instance.player.GetComponent<FPSController>();       // Get the FPSController script from the player
        obs.SetActive(false);
        hideInv = InventoryManager.Instance.inventory.GetComponent<RectTransform>();                // Get RectTransform of the inventory panel for resizing
        originalSize = hideInv.sizeDelta;                                                           // Store original size of inventory panel
        inventoryManager = InventoryManager.Instance;                                               // Cache reference to the InventoryManager
        journalOverlay.SetActive(false);
        inspectText.SetActive(false);
        inspectTextDescription.SetActive(false);
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
                inspectText.GetComponent<TextMeshProUGUI>().text = solidGoldCasketText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = solidGoldCasketText[1];
                Debug.Log("it's running the currentObservable if");
            }
            else if (currentObservable.name == "pivot2")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = modernCasketText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = modernCasketText[1];
            }
            else if (currentObservable.name == "pivot3")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = recycledCoffinText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = recycledCoffinText[1];
            }
            else if (currentObservable.name == "pivot4")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = fernBoquetText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = fernBoquetText[1];
            }
            else if (currentObservable.name == "pivot5")
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = roseBoquetText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = roseBoquetText[1];
            }
            else if (currentObservable.name == "pivot6") // tulips
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = tulipBoquetText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = tulipBoquetText[1];
            }
            else if (currentObservable.name == "pivot7") // orchids
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = orchidBoquetText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = orchidBoquetText[1];
            }
            else if (currentObservable.name == "pivot8") //lady p
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = portraitofLadyText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = portraitofLadyText[1];

            }
            else if (currentObservable.name == "pivot9") //child p
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = portraitofChildText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = portraitofChildText[1];
            }
            else if (currentObservable.name == "pivot10") // king p
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = portraitofKingText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = portraitofKingText[1];
            }
            else if (currentObservable.name == "pivot11") // broken crest
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = brokenFiligreeCrestText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = brokenFiligreeCrestText[1];
            }
            else if (currentObservable.name == "pivot12") // bugs
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = boxofBugsText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = boxofBugsText[1];
            }
            else if (currentObservable.name == "pivot13") // tallis
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = wovenShawlText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = wovenShawlText[1];

            }
            else if (currentObservable.name == "pivot14") // quilt
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = halfKnitQuiltText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = halfKnitQuiltText[1];
            }
            else if (currentObservable.name == "pivot15") // ledger
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = filigreeKeepLedgerText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = filigreeKeepLedgerText[1];
            }
            else if (currentObservable.name == "pivot16") // goblet
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = strippedGobletText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = strippedGobletText[1];
            }
            else if (currentObservable.name == "pivot17") // rusty key
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = rustyKeyText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = rustyKeyText[1];
            } else if (currentObservable.name == "pivot18") // quarters key
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = quartersKeyText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = quartersKeyText[1];
            } else if (currentObservable.name == "pivot19") // plank
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = woodenPlankText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = woodenPlankText[1];
            }
            else if (currentObservable.name == "pivot20") // his majesty
            {
                inspectText.GetComponent<TextMeshProUGUI>().text = hisMajestyText[0];
                inspectTextDescription.GetComponent<TextMeshProUGUI>().text = hisMajestyText[1];
            } 



            inspectText.SetActive(true);
            inspectTextDescription.SetActive(true);

            if (inspectText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on this GameObject!");
                return; // Prevent NullReferenceException
            }

            if (inspectTextDescription == null)
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
