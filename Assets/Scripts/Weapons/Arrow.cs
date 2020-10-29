using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Arrow : MonoBehaviour
{
	[SerializeField] private float startSpeed;
	[SerializeField] private float disappearingTime;

	[Header("Звуки")]
	[SerializeField] private AudioClip chpock;

	private bool isHit;
	private Rigidbody rigidbody;

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = transform.forward * startSpeed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isHit)
		{
			Joint(collision.collider);
			Audio.Instance.PlaySound(chpock, this.transform, true);
			isHit = true;
			var infected = collision.collider.GetComponent<Infected>();
			if (infected != null)
			{
				infected.Damage();
			}
			Destroy(this.gameObject, disappearingTime);
		}
	}

	private void Joint(Collider collider)
	{
		var rigidbody = collider.gameObject.GetComponent<Rigidbody>();
		if (rigidbody != null)
		{
			var conected = this.gameObject.AddComponent<FixedJoint>();
			conected.connectedBody = rigidbody;
		}
		else
		{
			this.rigidbody.isKinematic = true;
		}
	}
}
