using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour
{
	[SerializeField] private float maxHealth;
	[Tooltip("Частота ударов в секундах.")]
	[SerializeField] private float strikesFrequency;
	[SerializeField] private float findTargetRadius;
	[SerializeField] private LayerMask findTargetMask;

	private NavMeshAgent navMeshAgent;
	private float strikeTimer = 0f;
	private float health;

	private void Start()
	{
		health = maxHealth;
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (FindTarget(out var target))
		{
			navMeshAgent.SetDestination(target);
		}

		if (strikeTimer > 0)
			strikeTimer -= Time.deltaTime;
	}

	private bool FindTarget(out Vector3 target)
	{
		var foundTargets = Physics.OverlapSphere(transform.position, findTargetRadius, findTargetMask);
		if (foundTargets.Length > 0)
		{
			var foundTarget = foundTargets.First();
			target = foundTarget.transform.position;
			return true;
		}
		else
		{
			target = Vector3.zero;
			return false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		var human = other.GetComponent<IHuman>();
		if (human != null)
		{
			if (strikeTimer <= 0)
			{
				human.Damage();
				strikeTimer = strikesFrequency;
			}
		}
	}

	public void Damage()
	{
		health--;
		Debug.Log(String.Format("-1 здоровье зомби. \n Осталось: {0}", health));
	}
}
