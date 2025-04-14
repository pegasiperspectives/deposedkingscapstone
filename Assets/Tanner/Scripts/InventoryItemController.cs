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

    [SerializeField] private GameObject inventory;


    //this is started when inventory is opened on each inventory button




    void Awake()
   {
        
        
        
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
}
