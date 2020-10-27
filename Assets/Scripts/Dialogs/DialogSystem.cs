using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DialogSystem : MonoBehaviour
{
	[SerializeField] private GameObject dialogSample;

	private RectTransform rectTransform;

	public static DialogSystem Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();	
	}

	public void CreateDialog(Transform transform, string text, float lifetime = 0)
	{
		var dialogObject = Instantiate(dialogSample, rectTransform);
		var dialog = dialogObject.GetComponent<Dialog>();
		dialog.Show(transform, text, lifetime);
	}
}
