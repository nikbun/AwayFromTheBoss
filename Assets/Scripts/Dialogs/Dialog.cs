using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Dialog : MonoBehaviour
{
	[SerializeField] private DialogAnimator animator;
	[SerializeField] private GameObject image;
	[SerializeField] private TMP_Text TMPtext;
	[SerializeField] private float xShift;
	[SerializeField] private float yShift;

	private RectTransform rectTransform;
	private Transform target;
	private float scaleFactor;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (target != null)
		{
			FollowTarget();
		}
	}

	private void FollowTarget()
	{
		var camera = Camera.main;
		var position = camera.WorldToScreenPoint(target.position);
		rectTransform.position = new Vector3(position.x + xShift * scaleFactor, position.y + yShift * scaleFactor, position.z);
		image.SetActive(true);
	}

	public void Show(Transform target, string text, float lifetime = 0, float scaleFactor = 1f, bool isImportant = false)
	{
		this.target = target;
		TMPtext.text = text;
		this.scaleFactor = scaleFactor;
		animator.SetParameter(DialogAnimator.Bools.Important, isImportant);
		if (lifetime > 0)
		{
			StartCoroutine(Destroy(lifetime));
		}
	}

	public void Hide()
	{
		animator.SetParameter(DialogAnimator.Trigers.Hide);
		Destroy(this.gameObject, 1f);
	}

	private IEnumerator Destroy(float time)
	{
		yield return new WaitForSeconds(time);
		Hide();
	}
}
