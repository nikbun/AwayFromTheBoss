using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IHuman
{
	[SerializeField] private UsableCheker usableCheker;
	[SerializeField] private float maxHealth;
	[SerializeField] private float moveSpeed;

	private Rigidbody rigidbody;
	private float health;
	private Plane gazeLevelPlane;

	private void Start()
	{
		health = maxHealth;
		rigidbody = GetComponent<Rigidbody>();
		gazeLevelPlane = new Plane(Vector3.up, transform.position);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && usableCheker.IsUsable) 
			usableCheker.Use();

		if (Input.GetKeyDown(KeyCode.Mouse0))
			Shoot();

		RotateToMouse();
	}

	private void FixedUpdate()
	{
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

		if (direction.magnitude > 0)
		{
			rigidbody.velocity = direction * moveSpeed;
		}
	}

	private void RotateToMouse()
	{
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (gazeLevelPlane.Raycast(ray, out float hitdist))
			transform.LookAt(ray.GetPoint(hitdist));
	}

	private void Shoot()
	{
		if (Physics.Raycast(transform.position, transform.forward, out var hitInfo))
		{
			var zombie = hitInfo.collider.GetComponent<Zombie>();
			if (zombie != null)
				zombie.Damage();
		}
	}

	public void Damage()
	{
		health--;
		Debug.Log(String.Format("-1 здоровье игрока. \n Осталось: {0}", health));
	}
}
