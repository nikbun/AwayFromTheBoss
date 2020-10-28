using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PickUpQuestItem : MonoBehaviour, IPickUpItem
{
	[SerializeField] private string itemName;

	private QuestItem questItem;

	public UnityEvent PickUpEvent;

	private void Awake()
	{
		questItem = new QuestItem(itemName);
	}

	public void PickUp()
	{
		PickUpEvent?.Invoke();
		Destroy(this.gameObject);
	}

	public QuestItem GetQuestItem()
	{
		return questItem;
	}
}
