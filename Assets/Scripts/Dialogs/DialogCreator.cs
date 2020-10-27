using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DialogCreator : MonoBehaviour
{
	[SerializeField] private GameObject dialogSample;
	[SerializeField] private RectTransform canvasTransform;

	private RectTransform rectTransform;

	public static DialogCreator Instance { get; private set; }

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

	public Dialog CreateDialog(Transform transform, string text, float lifetime = 0, bool isImportant = false)
	{
		var dialogObject = Instantiate(dialogSample, rectTransform);
		var dialog = dialogObject.GetComponent<Dialog>();
		dialog.Show(transform, text, lifetime, canvasTransform.lossyScale.x, isImportant);
		return dialog;
	}
}
