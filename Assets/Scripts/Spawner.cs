using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private GameObject spawnObject;
	[SerializeField] private float minTimeSpawn;
	[SerializeField] private float maxTimeSpawn;

	private GameObject spavnedGameObject;

	private void Start()
	{
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
			if (spavnedGameObject == null)
			{
				spavnedGameObject = Instantiate(spawnObject, transform);
			}
		}
	}
}
