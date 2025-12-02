using UnityEngine;

public class whatever : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryItemController[] arrayOfIICs = FindObjectsByType<InventoryItemController>(FindObjectsInactive.Include, FindObjectsSortMode.None); 

        Debug.Log("heyyyyyyyy everyone");
        foreach (InventoryItemController i in arrayOfIICs)
        {
            Debug.Log("inventory item controller on object : " + i.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
