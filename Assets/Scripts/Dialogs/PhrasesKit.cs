using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhrasesKit : MonoBehaviour
{
	[SerializeField] private string nameKit;
	[SerializeField] private List<string> phrases;

	public UnityEvent EndKitEvent;

	private int index = 0;

	public bool IsEnded => index >= phrases.Count;

	public string GetNextPhrase()
	{
		if (index + 1 == phrases.Count)
		{
			EndKitEvent?.Invoke();
		}
		return phrases[index++];
	}

	public void ResetPhrases()
	{
		index = 0;
	}
}
