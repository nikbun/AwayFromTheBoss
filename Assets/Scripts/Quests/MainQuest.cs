using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuest : MonoBehaviour
{
	[SerializeField] private Inventory playerInventory;
	[SerializeField] private PickUpQuestItem benefitListPU;
	[SerializeField] private GameObject benefit;
	[SerializeField] private Exit exit;
	[SerializeField] private GameObject EndSplashScreen;
	[SerializeField] private Talker seregaTalker;
	[SerializeField] private Talker auntLubaTalker;
	[SerializeField] private Talker volodyaTalker;
	[Header("Нашел справку")]
	[SerializeField] private PhrasesKit seregaFindBenefitList;
	[Space]
	[SerializeField] private PhrasesKit auntLubaGetBenefit;
	[SerializeField] private PhrasesKit auntLubaGoHome;
	[Header("Получил пособие")]
	[SerializeField] private PhrasesKit seregaEndDay;
	[SerializeField] private PhrasesKit volodyaGoodBy;

	private QuestItem benefitList;

	public void TalkWithSerega()
	{

	}

	public void GetBenefitList() 
	{
		benefitList = benefitListPU.GetQuestItem();
		seregaTalker.AddPhrasesKit(seregaFindBenefitList, true);
		auntLubaTalker.AddPhrasesKit(auntLubaGetBenefit, true);
		auntLubaTalker.AddPhrasesKit(auntLubaGoHome);
	}

	public void TalkWithAuntLuba()
	{
		playerInventory.RemoveQuestItem(benefitList);
		benefit.SetActive(true);
	}

	public void GetBenefit()
	{
		seregaTalker.AddPhrasesKit(seregaEndDay, true);
		volodyaTalker.AddPhrasesKit(volodyaGoodBy, true);
		exit.OpenDoor();
	}

	public void GoHome()
	{
		EndSplashScreen.SetActive(true);
	}
}
