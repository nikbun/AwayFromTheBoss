using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(Collider))]
public class Infected : MonoBehaviour
{
	[SerializeField] private Collider collider;
	[SerializeField] private HumanAnimator animator;
	[SerializeField] private float maxHealth;
	[Tooltip("Частота ударов в секундах.")]
	[SerializeField] private float strikesFrequency;
	[SerializeField] private float findTargetRadius;
	[SerializeField] private LayerMask findTargetMask;
	[SerializeField] private RandomTalker randomTalker;

	private NavMeshAgent navMeshAgent;
	private float strikeTimer = 0f;
	private float health;
	private bool isDead;

	private const float destroyTime = 5f;

	private void Start()
	{
		health = maxHealth;
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (!isDead)
		{
			if (FindTarget(out var target))
			{
				navMeshAgent.SetDestination(target);
				randomTalker.CanTalk = true;
			}
			else
			{
				randomTalker.CanTalk = false;
			}
			animator.SetParameter(HumanAnimator.Bools.Walk, navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance);

			if (strikeTimer > 0)
			{
				strikeTimer -= Time.deltaTime;
			}
		}
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
		if (!isDead)
		{
			var human = other.GetComponent<IHuman>();
			if (human != null)
			{
				if (strikeTimer <= 0)
				{
					animator.SetParameter(HumanAnimator.Trigers.Sneeze);
					human.Damage();
					strikeTimer = strikesFrequency;
				}
			}
		}
	}

	public void Damage()
	{
		health--;
		if (!isDead && health <= 0)
		{
			Die();
		}
		Debug.Log(String.Format("-1 здоровье зомби. Осталось: {0}", health));
	}

	private void Die()
	{
		isDead = true;
		navMeshAgent.ResetPath();
		collider.enabled = false;
		animator.SetParameter(HumanAnimator.Trigers.Death);
		Destroy(this.gameObject, destroyTime);
	}
}
