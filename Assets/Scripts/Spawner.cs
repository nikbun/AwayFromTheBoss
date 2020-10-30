using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private GameObject spawnObject;
	[SerializeField] private float minTimeSpawn;
	[SerializeField] private float maxTimeSpawn;
	[SerializeField] private GameObject spavnedGameObject;
	[Tooltip("В присутствии кого нелься спавнить")]
	[SerializeField] private LayerMask notSpawnInPresenceMask;
	[Tooltip("Радиус поиска, того при ком нельзя спавнить")]
	[SerializeField] private float notSpawnInPresenceRadius;

	private void Start()
	{
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));

			var foundTargets = Physics.OverlapSphere(transform.position, notSpawnInPresenceRadius, notSpawnInPresenceMask);

			if (spavnedGameObject == null && foundTargets.Length == 0)
			{
				spavnedGameObject = Instantiate(spawnObject, transform);
			}
		}
	}
}
