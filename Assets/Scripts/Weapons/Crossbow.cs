using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour, IWeapon
{
	[SerializeField] private GameObject arrow;
	[SerializeField] private Vector3 localShootPoint;

	private int catrigeCount;

	public void Shoot()
	{
		Instantiate(arrow, transform.TransformPoint(localShootPoint), transform.rotation);
	}
}
