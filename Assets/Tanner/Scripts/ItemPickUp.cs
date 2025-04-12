using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
	public Item Item;
	//public bool canPickUp;
	public GameObject player;
    private FPSController fpscontrollerScript;
    

    private void Awake()
    {
        
        fpscontrollerScript = player.GetComponent<FPSController>();


    }
    void Pickup()
	{
		InventoryManager.Instance.Add(Item);
		Destroy(gameObject);

	}

	private void OnMouseDown()
	{
		if (FPSController.canPickUp == true)
		{
			Pickup();
		}
	}

}
