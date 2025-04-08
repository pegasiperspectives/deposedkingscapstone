using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	public static InventoryManager Instance;
	public List<Item> Items = new List<Item>();

	public Transform ItemContent;
	public GameObject InventoryItem;
	[SerializeField] private GameObject inventory;

	private void Awake()
	{
		Instance = this;
	}

	public void Add(Item item)
	{
		Items.Add(item);
	}

	//can add delete item here if needed
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E)) //Open inventory
		{
			if(inventory.activeInHierarchy == false)
			{
				inventory.SetActive(true);
				ListItems();
			}
			else if(inventory.activeInHierarchy == true)
			{
				inventory.SetActive(false);
				CleanItems();
			}
		}
	}

	public void ListItems() //set items in inventory
	{
		foreach (Transform item in ItemContent)
		{
			Destroy(item.gameObject);
		}

		foreach (var item in Items)
		{
			GameObject obj = Instantiate(InventoryItem, ItemContent);
			var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
			var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

			itemName.text = item.itemName;
			itemIcon.sprite = item.icon;
		}
	}
	public void CleanItems() //gets rid of duplicates when reopening inventory
	{
		foreach (Transform item in ItemContent)
		{
			Destroy(item.gameObject);
		}
	}

}
