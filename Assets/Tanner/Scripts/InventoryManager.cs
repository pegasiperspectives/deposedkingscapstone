using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;                        // instance so other scripts can easily access the inventory

    [SerializeField]
    public MenuManager mm;

    public List<Item> Items = new List<Item>();                     // Master list of all items currently in the inventory

    public InventoryItemController iic;                             // Reference to individual item controllers (used when listing items)




    public static bool currentlyInspecting = false;                 // Flag used when inspecting an item (to disable certain controls)

    // References to important scene objects
    public GameObject player;
    public Transform ItemContent;                                   // UI container for item entries
    public InventoryItemController[] InventoryItems;                // Items array
    public GameObject InventoryItem;                                // Prefab for a single item UI element



    [SerializeField] public GameObject inventory;                   // Inventory UI panel
    [SerializeField] private GameObject dialogue;                   // Dialogue UI panel

    // Player control and placement references
    private FPSController fpscontrollerScript;
    public PlaceObjects placeObjects;


    public ObsCamera obscamera;                                     // Reference to camera used for inspecting objects

    public Transform rig;                                           // Unused here but may reference camera rig

    // References to observable 3D objects for inspection
    [SerializeField] public GameObject ObservableObject1;
    [SerializeField] public GameObject ObservableObject2;
    [SerializeField] public GameObject ObservableObject3;
    [SerializeField] public GameObject ObservableObject4;
    [SerializeField] public GameObject ObservableObject5;
    [SerializeField] public GameObject ObservableObject6;
    [SerializeField] public GameObject ObservableObject7;
    [SerializeField] public GameObject ObservableObject8;
    [SerializeField] public GameObject ObservableObject9;



    [SerializeField] public GameObject Item1;
    [SerializeField] public GameObject Item2;
    [SerializeField] public GameObject Item3;
    [SerializeField] public GameObject Item4;
    [SerializeField] public GameObject Item5;
    [SerializeField] public GameObject Item6;
    [SerializeField] public GameObject Item7;
    [SerializeField] public GameObject Item8;
    [SerializeField] public GameObject Item9;
    [SerializeField] public GameObject Item10;
    [SerializeField] public GameObject Item11;
    [SerializeField] public GameObject Item12;
    [SerializeField] public GameObject Item13;
    [SerializeField] public GameObject Item14;
    [SerializeField] public GameObject Item15;
    [SerializeField] public GameObject Item16;
    [SerializeField] public GameObject Item17;
    [SerializeField] public GameObject Item18;
    [SerializeField] public GameObject Item19;
    [SerializeField] public GameObject Item20;
    [SerializeField] public GameObject Item21;

    public bool inventoryOpen = false;

    // Audio for item pickup
    private AudioSource audioSource;
    public AudioClip pickupsound;
    [SerializeField] private Camera camobj;     // Camera that holds the audio source

    private void Awake()
    {
        Instance = this;                                                // Set up singleton instance
        fpscontrollerScript = player.GetComponent<FPSController>();     // Grab reference to FPSController script on player
        obscamera.gameObject.SetActive(false);                          // Make sure inspection camera starts off

        audioSource = camobj.GetComponent<AudioSource>();               // Get audio source from the specified camera

        Item1.SetActive(true);
        Item2.SetActive(false);
        Item3.SetActive(false);
        Item4.SetActive(false);
        Item5.SetActive(false);
        Item6.SetActive(false);
        Item7.SetActive(false);
        Item8.SetActive(false);
        Item9.SetActive(false);
        Item10.SetActive(false);
        Item11.SetActive(false);
        Item12.SetActive(false);
        Item13.SetActive(false);
        Item14.SetActive(false);
        Item15.SetActive(false);
        Item16.SetActive(false);
        Item17.SetActive(false);
        Item18.SetActive(false);
        Item19.SetActive(false);
        Item20.SetActive(false);
        Item21.SetActive(false);
    }


    // Add an item to the inventory list
    public void Add(Item item)
    {
        Items.Add(item);
    }


    private void Update()
    {
        // If neither inventory nor dialogue are open, allow placing objects
        if (inventory.activeInHierarchy == false && dialogue.activeInHierarchy == false)
        {
            placeObjects.canPlace = true;
        }
        // If either inventory or dialogue is open, prevent placing objects
        else if (inventory.activeInHierarchy == true || dialogue.activeInHierarchy == true)
        {
            placeObjects.canPlace = false;
        }

        // Open or close the inventory with the E key (when not inspecting)
        if (Input.GetKeyDown(KeyCode.Tab) && currentlyInspecting == false) //Open/close inventory
        {

            // If inventory is closed and no special placement modes are active
            if (inventory.activeInHierarchy == false && PlaceObjects.placeIsExample1 == false && PlaceObjects.placeIsExample2 == false && dialogue.activeInHierarchy == false && PlaceObjects.placeIsExample3 == false && PlaceObjects.placeIsFern == false && PlaceObjects.placeIsRoses == false && PlaceObjects.placeIsTulips == false && PlaceObjects.placeIsOrchids == false && PlaceObjects.placeIsLadyPort == false && PlaceObjects.placeIsChildPort == false)
            {
                // Open inventory
                placeObjects.canPlace = true;
                inventoryOpen = true;

                inventory.SetActive(true);
                ListItems();
                CursorOn();                        // Unlock cursor                           // Refresh list
                fpscontrollerScript.canMove = false;    // Freeze player
                FPSController.canPickUp = false;        // Disable pickup
            }

            // If inventory is already open, close it
            else if (inventory.activeInHierarchy == true)
            {
                placeObjects.canPlace = false;
                inventoryOpen = false;
                inventory.SetActive(false);
                //CleanItems();                           // Clear UI objects
                CursorOff();                         // Unlock cursor  
                fpscontrollerScript.canMove = true;     // Allow movement
                FPSController.canPickUp = true;         // Re-enable pickup
            }
        }
    }

    // Rebuild inventory UI list based on Items list
    public void ListItems()
    {
        // Clear any existing UI entries
        //   foreach (Transform item in ItemContent)
        //  {
        //      Destroy(item.gameObject);
        //  }

        // Create a UI entry for each item in Items
        foreach (var item in Items)
        {
            
            if (item.id == 1) //gold coffin
            {
                Item2.SetActive(true);
                var itemController = Item2.GetComponentInChildren<InventoryItemController>();
                itemController.AddItem(item);
            }
            else if (item.id == 2) //modern coffin
            {
                Item3.SetActive(true);
                var itemController = Item3.GetComponentInChildren<InventoryItemController>();
                itemController.AddItem(item);
            }
            else if (item.id == 3) //recycled coffin
            {
                Item4.SetActive(true);
                var itemController = Item4.GetComponentInChildren<InventoryItemController>();
                itemController.AddItem(item);
            }
            else if (item.id == 4) //fern
            {
                Item5.SetActive(true);
                var itemController = Item5.GetComponentInChildren<InventoryItemController>();
                itemController.AddItem(item);
            }
            else if (item.id == 5) //roses
            {
                Item6.SetActive(true);
                var itemController = Item6.GetComponentInChildren<InventoryItemController>();
                itemController.AddItem(item);
            }
            else if (item.id == 6)
            {
                Item8.SetActive(true);
                var itemController = Item8.GetComponentInChildren<InventoryItemController>();
                itemController.AddItem(item);
            }
            else if (item.id == 7)
            {
                Item7.SetActive(true);
                var itemController = Item7.GetComponentInChildren<InventoryItemController>();
                itemController.AddItem(item);
            }

            //    GameObject obj = Instantiate(InventoryItem, ItemContent);

            // Fill in UI fields (name and icon)
            //var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            //var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            //itemName.text = item.itemName;
            //itemIcon.sprite = item.icon;

            //  IMPORTANT: Set the item on the button
            // Assign the item to the UI controller
            //var itemController = obj.GetComponent<InventoryItemController>();
            //itemController.AddItem(item);
        }
        // Refresh array of InventoryItemControllers
        SetInventoryItems();
    }

    // Clean up UI entries (called when closing inventory)
    /*  public void CleanItems() //gets rid of duplicates when reopening inventory
      {
          foreach (Transform item in ItemContent)
          {
              Destroy(item.gameObject);
          }
      }*/

    // When pressing the close inventory button in UI
    public void CloseInventoryButton()
    {
        CursorOff();
        fpscontrollerScript.canMove = true;
        FPSController.canPickUp = true;

    }

    // When pressing the open inventory button in UI
    public void OpenInventoryButton()
    {
        CursorOn();
        fpscontrollerScript.canMove = false;
        FPSController.canPickUp = false;
    }

    public void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CursorOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Remove an item from the inventory
    public void Remove(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);

            ListItems(); // Refresh inventory UI

        }
        else
        {
            Debug.LogWarning("Tried to remove item not in inventory: " + item.itemName);
        }
    }

    // Updates InventoryItems array and rebinds data
    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }

    // Turns off inventory in case you need to switch to placement mode
    public void turnoffinventorygorplace()
    {
        placeObjects.canPlace = true;
    }


    // Sets placement mode active for a specific object
    public void ActiveThing()
    {

        PlaceObjects.placeIsExample1 = true;

    }


    // Closes the inventory UI (used by other scripts)
    public void TurnoffInv()
    {
        inventory.SetActive(false);
    }


    // Plays a pickup sound effect
    public void playpickupsound()
    {
        audioSource.PlayOneShot(pickupsound);
    }
}




