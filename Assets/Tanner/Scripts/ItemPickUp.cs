using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
	public Item Item;
	//public bool canPickUp;
	public GameObject player;
    private FPSController fpscontrollerScript;
	public Camera camera;
    private RaycastHit hit;
    private void Awake()
    {
        
        fpscontrollerScript = player.GetComponent<FPSController>();
		camera = Camera.main;

    }
    void Pickup()
	{
		InventoryManager.Instance.Add(Item);
		Destroy(gameObject);

	}
    private void Update()
    {
        //Vector3 origin = camera.transform.position;
        //Vector3 direction
    }
    private void OnMouseDown()
	{
		if (FPSController.canPickUp == true)
		{
            //RaycastHit hit;

            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
			{
                Pickup();
            }
			
		}
	}

}
