using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckQuest : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private RubberDuckController rubberDuckController;
	[SerializeField] private RubberDuck duckNatasha;
	[SerializeField] private Talker anyaTalker;
	[SerializeField] private PhrasesKit anyaFindNatasha;
	[SerializeField] private PhrasesKit anyaLast;
	[SerializeField] private Talker antonTalker;
	[SerializeField] private PhrasesKit antonCatchDuck;
	[SerializeField] private PhrasesKit antonSilence;
	[SerializeField] private AudioClip rubberDuckSound;


	private QuestItem rubberDuck;

	public void CatchDuck()
	{
		rubberDuck = new QuestItem("Резиновая уточка"); // TODO: Локализация
		inventory.AddQuestItem(rubberDuck); 
		rubberDuckController.StopPeepOut();
		anyaTalker.AddPhrasesKit(anyaFindNatasha, true);
		anyaTalker.AddPhrasesKit(anyaLast);
		antonTalker.AddPhrasesKit(antonCatchDuck, true);
	}

	public void TalkAnton()
	{
		Audio.Instance.PlaySound(rubberDuckSound, inventory.transform, true);
	}

	public void TalkAnya()
	{
		inventory.RemoveQuestItem(rubberDuck);
		duckNatasha.gameObject.SetActive(true);
		duckNatasha.Use();
		antonTalker.AddPhrasesKit(antonSilence, true);
	}
}
