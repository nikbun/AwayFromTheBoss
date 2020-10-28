﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IHuman
{
	[SerializeField] private HumanAnimator animator;
	[SerializeField] private UsableCheker usableCheker;
	[SerializeField] private int maxMascCount;
	[SerializeField] private float moveSpeed;
	[Space]
	[SerializeField] private Crossbow crossbow;

	private Rigidbody rigidbody;
	private int mascCount;
	private Plane gazeLevelPlane;

	/// <summary>
	/// Текущее и максимальное число
	/// </summary>
	public event UnityAction<int, int> UpdateMaskCount;

	private void Start()
	{
		mascCount = maxMascCount;
		UpdateMaskCount(mascCount, maxMascCount);
		rigidbody = GetComponent<Rigidbody>();
		gazeLevelPlane = new Plane(Vector3.up, transform.position);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && usableCheker.IsUsable)
		{
			usableCheker.Use();
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Attack();
		}

		RotateToMouse();
	}

	private void FixedUpdate()
	{
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

		var isWalk = direction.magnitude > 0;
		animator.SetParameter(HumanAnimator.Bools.Walk, isWalk);
		if (isWalk)
		{
			rigidbody.velocity = direction * moveSpeed;
		}
	}

	private void RotateToMouse()
	{
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (gazeLevelPlane.Raycast(ray, out float hitdist))
		{
			transform.LookAt(ray.GetPoint(hitdist));
		}
	}

	private void Attack()
	{
		crossbow.Shoot();
	}

	public void Damage()
	{
		if (mascCount == 0)
		{
			Die();
		}
		else
		{
			mascCount--;
			UpdateMaskCount?.Invoke(mascCount, maxMascCount);
		}
	}

	private void Die()
	{
		Debug.Log("Конец игры!");
	}
}
