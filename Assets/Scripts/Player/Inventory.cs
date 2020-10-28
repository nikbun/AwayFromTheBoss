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
			questItems.Add(questItem.GetQuestItem());
			questItem.PickUp();
			return;
		}
	}

	public void RemoveQuestItem(QuestItem item)
	{
		questItems.Remove(item);
	}
}
