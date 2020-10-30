using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberDuckController : MonoBehaviour
{
	[SerializeField] private RubberDuckAnimator leftColumnAnimator;
	[SerializeField] private RubberDuckAnimator rightColumnAnimator;

	private Coroutine peepOutCoroutine;

	private void Start()
	{
		peepOutCoroutine = StartCoroutine(PeepOut());
	}

	public IEnumerator PeepOut()
	{
		while (true)
		{
			int peepOutNumber = Random.Range(1, 5);

			//Отключаем лишние аниматоры
			switch (peepOutNumber)
			{
				case 1:
				case 2:
					leftColumnAnimator.gameObject.SetActive(true);
					rightColumnAnimator.gameObject.SetActive(false);
					break;
				case 3:
				case 4:
					leftColumnAnimator.gameObject.SetActive(false);
					rightColumnAnimator.gameObject.SetActive(true);
					break;
			}

			//Выбираем сторону
			switch (peepOutNumber)
			{
				case 1:
				case 3:
					var peepOutLeft = RubberDuckAnimator.Trigers.PeepOutLeft;
					leftColumnAnimator.SetParameter(peepOutLeft);
					rightColumnAnimator.SetParameter(peepOutLeft);
					break;
				case 2:
				case 4:
					var peepOutRight = RubberDuckAnimator.Trigers.PeepOutRight;
					leftColumnAnimator.SetParameter(peepOutRight);
					rightColumnAnimator.SetParameter(peepOutRight);
					break;
			}
			yield return new WaitForSeconds(2f);
		}
	}

	public void StopPeepOut()
	{
		StopCoroutine(peepOutCoroutine);
		Destroy(this.gameObject);
	}

	
}
