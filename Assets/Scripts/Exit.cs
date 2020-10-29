using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
	[SerializeField] private GameObject closedDoor;
	[SerializeField] private GameObject openedDoor;

	public  UnityEvent PlayerExitEvent;

	private void OnTriggerEnter(Collider other)
	{
		var player = other.gameObject.GetComponent<Player>();
		if (player != null)
		{
			PlayerExitEvent?.Invoke();
		}
	}

	public void OpenDoor()
	{
		closedDoor.SetActive(false);
		openedDoor.SetActive(true);
	}
}