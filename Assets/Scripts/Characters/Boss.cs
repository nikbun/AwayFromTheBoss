using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

/// <summary>
/// Этот скрипт я назвал - "Прощай номинация на чистый код")
/// </summary>
[RequireComponent(typeof(NavMeshAgent), typeof(Collider))]
public class Boss : MonoBehaviour
{
	[SerializeField] private Collider collider;
	[SerializeField] private HumanAnimator animator;
	[SerializeField] private float maxHealth;
	[Tooltip("Частота ударов в секундах.")]
	[SerializeField] private float strikesFrequency;
	[SerializeField] private float findTargetRadius;
	[SerializeField] private LayerMask findTargetMask;
	[Tooltip("Через что нельзя увидеть")]
	[SerializeField] private LayerMask OpaqueMask;
	[SerializeField] private RandomTalker randomTalker;
	[Header("Свободный режим")]
	[SerializeField] private float minWaitTime;
	[SerializeField] private float maxWaitTime;
	[SerializeField] private float minWalkDistance;
	[SerializeField] private float maxWalkDistance;

	private NavMeshAgent navMeshAgent;
	private float strikeTimer = 0f;
	private float health;
	private bool isDead;

	private const float destroyTime = 5f;

	private bool isWait;
	private bool isWalk;

	private void Start()
	{
		health = maxHealth;
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (!isDead)
		{
			Vector3 target;
			if (FindTarget(out target))
			{
				navMeshAgent.SetDestination(target);
				randomTalker.CanTalk = true;
			}
			else
			{
				randomTalker.CanTalk = false;
			}

			var oldIsWalk = isWalk;
			isWalk = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

			animator.SetParameter(HumanAnimator.Bools.Walk, isWalk);

			if (oldIsWalk && !isWalk)
			{
				StartCoroutine(Wait(Random.Range(minWaitTime, maxWaitTime)));
			}
			if (!isWait && !isWalk && RandomWalk(out target))
			{
				navMeshAgent.SetDestination(target);
			}


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
			if (Physics.Raycast(
				new Ray(transform.position, foundTarget.transform.position - transform.position), 
				out var hitInfo,
				1000,
				OpaqueMask + findTargetMask,
				QueryTriggerInteraction.Ignore))
			{
				if (hitInfo.collider == foundTarget)
				{
					target = foundTarget.transform.position;
					return true;
				}
			}
		}
		target = Vector3.zero;
		return false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (!isDead)
		{
			var player = other.GetComponent<Player>();
			if (player != null)
			{
				if (strikeTimer <= 0)
				{
					animator.SetParameter(HumanAnimator.Trigers.Sneeze);
					player.Die();
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
		Debug.Log(System.String.Format("-1 здоровье зомби. Осталось: {0}", health));
	}

	private void Die()
	{
		isDead = true;
		navMeshAgent.ResetPath();
		collider.enabled = false;
		animator.SetParameter(HumanAnimator.Trigers.Death);
		Destroy(this.gameObject, destroyTime);
	}

	private bool RandomWalk(out Vector3 target)
	{
		var direction = new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)).normalized;
		var distance = Random.Range(minWalkDistance, maxWalkDistance);

		if (Physics.Raycast(transform.position, direction, out var hit, distance, OpaqueMask))
		{
			distance = Vector3.Distance(transform.position, hit.point);
		}

		target = transform.position + direction * distance;
		return true;
	}

	private IEnumerator Wait(float seconds)
	{
		isWait = true;
		yield return new WaitForSeconds(seconds);
		isWait = false;
	}
}
