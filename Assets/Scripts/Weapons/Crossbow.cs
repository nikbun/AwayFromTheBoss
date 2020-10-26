using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour, IWeapon
{
	[SerializeField] private GameObject arrow;
	[SerializeField] private Vector3 localShootPoint;

	[Header("Звуки")]
	[SerializeField] private AudioClip shoot;

	private int catrigeCount;

	public void Shoot()
	{
		Audio.Instance.PlaySound(shoot, this.transform);
		Instantiate(arrow, transform.TransformPoint(localShootPoint), transform.rotation);
	}
}
