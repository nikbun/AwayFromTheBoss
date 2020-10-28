using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaskIndicator : MonoBehaviour
{
	[SerializeField] private Player player;
	[SerializeField] private TMP_Text TMPText;

	private void Awake()
	{
		player.UpdateMaskCount += UpdateMaskCount;
	}

	public void UpdateMaskCount(int count, int maxCount)
	{
		TMPText.text = string.Format("Маски - {0}/{1}", count, maxCount);
	}
}
