using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Mask : MonoBehaviour, IPickUpItem
{
	public void PickUp()
	{
		Destroy(this.gameObject);
	}
}
