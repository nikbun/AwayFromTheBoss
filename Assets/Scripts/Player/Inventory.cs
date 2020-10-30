using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
	[SerializeField] private int maskCount;
	[SerializeField] private int maxMaskCount;

	private List<QuestItem> questItems = new List<QuestItem>();

	public event UnityAction UpdateMaskCount;
	public event UnityAction UpdateInventory;

	public int MaskCount
	{
		get => maskCount;
		set
		{
			maskCount = value;
			UpdateMaskCount?.Invoke();
		}
	}

	public int MaxMaskCount => maxMaskCount;

	public IEnumerable<QuestItem> Items => questItems;


	private void Start()
	{
		UpdateMaskCount?.Invoke();
	}

	public void PickUpItem(IPickUpItem item)
	{
		var mask = item as Mask;
		if (mask != null && MaskCount < MaxMaskCount)
		{
			mask.PickUp();
			MaskCount++;
			return;
		}

		var questItem = item as PickUpQuestItem;
		if (questItem != null)
		{
			AddQuestItem(questItem.GetQuestItem());
			questItem.PickUp();
			return;
		}
	}

	public void AddQuestItem(QuestItem item)
	{
		questItems.Add(item);
		UpdateInventory?.Invoke();
	}

	public void RemoveQuestItem(QuestItem item)
	{
		questItems.Remove(item);
		UpdateInventory?.Invoke();
	}
}
