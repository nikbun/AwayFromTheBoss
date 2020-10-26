using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Arrow : MonoBehaviour
{
	[SerializeField] private float startSpeed;
	[SerializeField] private float disappearingTime;

	private bool isHit;

	private const float destroyTime = 5f;

	private void Start()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * startSpeed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isHit)
		{
			isHit = true;
			var infected = collision.collider.GetComponent<Infected>();
			if (infected != null)
			{
				infected.Damage();
			}
			Destroy(this.gameObject, destroyTime);
		}
	}


}
