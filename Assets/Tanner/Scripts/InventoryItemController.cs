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
    //public GameObject placeobj;
   void Update()
   {
       //Debug.Log(placeobj.GetComponentInChildren<PlaceObjects>().placeIsExample1);
   }

    public void AddItem(Item newItem)
    {
        item = newItem;
       

    }
    public void RemoveItem() //removing from inventory list; not to be accessed again
    {
        InventoryManager.Instance.Remove(item);
        
        Destroy(gameObject);
    }

    //public void UseItem()
    //{
    //    Debug.Log(item.id);
    //    if (item.id == 1)
    //    {
    //        placeobj.GetComponentInChildren<PlaceObjects>().placeIsExample1 = true;
    //    }
    //    else if (item.id == 2)
    //    {
    //        Debug.Log("hello2");
    //    }
    //    RemoveItem();

    //}
    
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
            //if (placeobj == null)
            //{
            //    Debug.LogError("placeobj is not assigned!");
            //    return;
            //}

            //var placer = placeobj.GetComponentInChildren<PlaceObjects>();
            //var placer = placeobj.GetComponent<Camera>().GetComponent<PlaceObjects>();
            var placer = placeObjects;
            if (placer != null)
            {
                Debug.Log("Changing placeIsExample1 to TRUE.");
                //placer.placeIsExample1 = true;
                //placeobj.GetComponentInChildren<PlaceObjects>().switchBool();
                //placeObjects.SwitchBool();
                //checkthis = true;
                Debug.Log(placeObjects.placeIsExample1);

                //Debug.Log(placeobj.GetComponentInChildren<PlaceObjects>().placeIsExample1);



            }
            else
            {
                Debug.LogError("PlaceObjects component not found in placeobj.");
            }
        }

       RemoveItem();
    }
}
