using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Talker : MonoBehaviour, IUsable
{
	[SerializeField] private bool isImportant;
	[SerializeField] private float lifetimeDialog;
	[SerializeField] private AudioClip talk;
	[SerializeField] private List<PhrasesKit> phraseKits;

	private Dialog currentDialog;

	private PhrasesKit CurrentPhrasesKit => phraseKits.First();

	public void Use()
	{
		if (CurrentPhrasesKit.IsEnded)
		{
			if (phraseKits.Count > 1)
			{
				phraseKits.Remove(CurrentPhrasesKit);
			}
			else
			{
				CurrentPhrasesKit.ResetPhrases();
			}
			currentDialog.Hide();
		}
		else
		{
			if (currentDialog != null)
			{
				currentDialog.Hide();
			}
			var phrase = CurrentPhrasesKit.GetNextPhrase();
			if (phrase != "")
			{
				Audio.Instance.PlaySound(talk, this.transform, true);
				currentDialog = DialogCreator.Instance.CreateDialog(this.transform, phrase, lifetimeDialog, isImportant);
			}
		}
	}

	public void AddPhrasesKit(PhrasesKit kit, bool cleatOld = false)
	{
		if (cleatOld)
		{
			phraseKits.Clear();
		}
		phraseKits.Add(kit);
	}
}
