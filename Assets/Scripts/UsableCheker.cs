using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableCheker : MonoBehaviour
{
	private IUsable usable;

	public bool IsUsable => usable != null;

	private void OnTriggerEnter(Collider other)
	{
		var findingUsable = other.GetComponent<IUsable>();
		if (findingUsable != null)
		{
			usable = findingUsable;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var findingUsable = other.GetComponent<IUsable>();
		if (findingUsable == usable)
		{
			usable = null;
		}
	}

	public void Use()
	{
		usable.Use();
	}
}
