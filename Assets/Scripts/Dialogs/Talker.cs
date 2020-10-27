using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour, IUsable
{
	[SerializeField] private bool isImportant;
	[SerializeField] private float lifetimeDialog;
	[SerializeField] private AudioClip talk;
	[SerializeField] private List<string> phrases;

	private int indexPhrase;
	private Dialog currentDialog;

	public void Use()
	{
		if (indexPhrase >= phrases.Count)
		{
			indexPhrase = 0;
			currentDialog.Hide();
		}
		else
		{
			if (currentDialog != null)
			{
				currentDialog.Hide();
			}
			Audio.Instance.PlaySound(talk, this.transform, true);
			currentDialog = DialogCreator.Instance.CreateDialog(this.transform, phrases[indexPhrase], lifetimeDialog, isImportant);
			indexPhrase++;
		}
	}
}
