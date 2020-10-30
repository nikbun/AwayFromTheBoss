using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UsableCheker : MonoBehaviour
{
	private IUsable usable;

	public bool IsUsable => !usable.Equals(null);/*usable != null;*/ //TODO: Баг, поле ссылается на удаленный объект

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
