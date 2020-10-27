using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTalker : MonoBehaviour
{
	[SerializeField] private bool canTalk;
	[SerializeField] private float lifetimeDialog;
	[SerializeField] private float minTime;
	[SerializeField] private float maxTime;
	[SerializeField] private AudioClip talk;
	[SerializeField] private List<string> phrases;

	private float timer;

	public bool CanTalk 
	{ 
		get => canTalk; 
		set 
		{
			if (canTalk = false && value == true)
			{
				timer = Random.Range(minTime, maxTime);
			}
			canTalk = value;
		}  
	}

	private void Update()
	{
		if (canTalk)
		{
			if (timer > 0)
			{
				timer -= Time.deltaTime;
			}
			else
			{
				Audio.Instance.PlaySound(talk, this.transform, true);
				DialogCreator.Instance.CreateDialog(this.transform, phrases[Random.Range(0, phrases.Count)], lifetimeDialog);
				timer = Random.Range(minTime, maxTime);
			}
			
		}
	}
}
