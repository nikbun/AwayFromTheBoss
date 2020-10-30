using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaskIndicator : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private TMP_Text TMPText;

	private void OnEnable()
	{
		inventory.UpdateMaskCount += UpdateMaskCount;
	}

	private void OnDisable()
	{
		inventory.UpdateMaskCount -= UpdateMaskCount;
	}

	public void UpdateMaskCount()
	{
		TMPText.text = string.Format("Маски - {0}/{1}", inventory.MaskCount, inventory.MaxMaskCount); // TODO Локализация
	}
}
