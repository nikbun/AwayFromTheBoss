using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class InventoryOverview : MonoBehaviour
{
	[SerializeField] private GameObject sampleInventoryItem;
	[SerializeField] private GameObject listItems;
	[SerializeField] private Inventory inventory;

	private Stack<GameObject> list = new Stack<GameObject>();

	private void OnEnable()
	{
		inventory.UpdateInventory += UpdateList;
	}
	private void OnDisable()
	{
		inventory.UpdateInventory -= UpdateList;
	}

	public void AddItem(QuestItem questItem)
	{
		var listItem = Instantiate(sampleInventoryItem, listItems.transform);
		var TMPtext = listItem.GetComponent<TMP_Text>();
		TMPtext.text = questItem.name;
		listItem.SetActive(true);
		list.Push(listItem);
	}

	public void ClearList()
	{
		while (list.Count > 0)
		{
			Destroy(list.Pop());
		}
	}

	private void UpdateList()
	{
		ClearList();
		foreach(var item in inventory.Items)
		{
			AddItem(item);
		}
	}
}
