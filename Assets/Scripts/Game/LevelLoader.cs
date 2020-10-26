using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	[SerializeField] private GameObject audio;


	private void Awake()
	{
		Instantiate(audio);
	}

	private void Start()
	{
		Audio.Instance.PlayGameMusic();
	}
}
