using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public InventoryItemController iic;

   


    public static bool currentlyInspecting = false;
    public GameObject player;
    public Transform ItemContent;
    public InventoryItemController[] InventoryItems;
    public GameObject InventoryItem;

    [SerializeField] public GameObject inventory;
    [SerializeField] private GameObject dialogue;

    private FPSController fpscontrollerScript;
    public PlaceObjects placeObjects;

    public ObsCamera obscamera;

    public Transform rig;

    [SerializeField] public GameObject ObservableObject1;

    [SerializeField] public GameObject ObservableObject2;

    [SerializeField] public GameObject ObservableObject3;
    [SerializeField] public GameObject ObservableObject4;
    [SerializeField] public GameObject ObservableObject5;
    [SerializeField] public GameObject ObservableObject6;
    [SerializeField] public GameObject ObservableObject7;
    [SerializeField] public GameObject ObservableObject8;
    [SerializeField] public GameObject ObservableObject9;

    private AudioSource audioSource;
    public AudioClip pickupsound;
    [SerializeField] private Camera camobj;

    private void Awake()
    {
        Instance = this;
        fpscontrollerScript = player.GetComponent<FPSController>(); //call other script
        obscamera.gameObject.SetActive(false);
        
        audioSource = camobj.GetComponent<AudioSource>();
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    
    private void Update()
    {
        if (inventory.activeInHierarchy == false && dialogue.activeInHierarchy == false)
        {
            placeObjects.canPlace = true;
        }
        else if(inventory.activeInHierarchy == true || dialogue.activeInHierarchy == true)
        {
            placeObjects.canPlace = false;
        }
      

        if (Input.GetKeyDown(KeyCode.E) && currentlyInspecting == false) //Open/close inventory
        {
            if (inventory.activeInHierarchy == false && PlaceObjects.placeIsExample1 == false && PlaceObjects.placeIsExample2 == false && dialogue.activeInHierarchy == false)
            {
                placeObjects.canPlace = true;

                inventory.SetActive(true);
                ListItems();
                ToggleCursor();
                fpscontrollerScript.canMove = false;
                
                FPSController.canPickUp = false;
            }
            else if (inventory.activeInHierarchy == true)
            {
                placeObjects.canPlace = false;
                inventory.SetActive(false);
                CleanItems();
                ToggleCursor();
                fpscontrollerScript.canMove = true;
                
                FPSController.canPickUp = true;
            }
        }
    }

    
    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            // Set text/icon
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            //  IMPORTANT: Set the item on the button
            var itemController = obj.GetComponent<InventoryItemController>();
            itemController.AddItem(item);
        }

        SetInventoryItems();
    }
    public void CleanItems() //gets rid of duplicates when reopening inventory
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
    public void CloseInventoryButton()
    {
        ToggleCursor();
        fpscontrollerScript.canMove = true;
        FPSController.canPickUp = true;
        
    }
    public void OpenInventoryButton()
    {
        ToggleCursor();
        fpscontrollerScript.canMove = false;
       
        FPSController.canPickUp = false;
    }
    public void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
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
    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
    public void turnoffinventorygorplace()
    {
        placeObjects.canPlace = true;
    }



    public void ActiveThing()
    {

        PlaceObjects.placeIsExample1 = true;

    }


    public void TurnoffInv()
    {
        inventory.SetActive(false);
    }

    public void playpickupsound()
    {
        audioSource.PlayOneShot(pickupsound);
    }
}




