using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public InventoryManager inventoryManager;
    //public GameObject inventoryManagerObj;
    public PlaceObjects placeObjects;
    public bool checkthis = false;
    public GameObject placeobj;
    



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
        
        Destroy(gameObject);
    }

    

    public void UseItem()
    {
        
        if (item == null)
        {
            Debug.LogWarning("Item is null in UseItem!");
            return;
        }

        Debug.Log("Using item with ID: " + item.id);

        if (item.id == 1)
        {
            
            var placer = placeObjects;

                Debug.Log(placeObjects.placeIsExample1 + "itemID");
            placeObjects.placeIsExample1 = true;
            Debug.Log(placeObjects.placeIsExample1 + "itemID2");
            
        }
        else if (item.id == 2)
        {

        }


        RemoveItem();
        
    }

   
}
