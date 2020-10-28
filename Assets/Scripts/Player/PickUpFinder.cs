using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PickUpFinder : MonoBehaviour
{
	public event UnityAction<IPickUpItem> PickUpEvent;

	private void OnTriggerEnter(Collider other)
	{
		var findingPickUpItem = other.GetComponent<IPickUpItem>();
		if (findingPickUpItem != null)
		{
			PickUpEvent?.Invoke(findingPickUpItem);
		}
	}
}
