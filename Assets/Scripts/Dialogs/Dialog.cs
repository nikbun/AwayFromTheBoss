using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Dialog : MonoBehaviour
{
	[SerializeField] private GameObject image;
	[SerializeField] private TMP_Text TMPtext;
	[SerializeField] private float xShift;
	[SerializeField] private float yShift;

	private RectTransform rectTransform;
	private Transform target;

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
		var position = Camera.main.WorldToScreenPoint(target.position);
		rectTransform.position = new Vector3(position.x + xShift, position.y + yShift, position.z);
		image.SetActive(true);
	}

	public void Show(Transform target, string text, float lifetime = 0)
	{
		this.target = target;
		TMPtext.text = text;

		if (lifetime > 0)
		{
			Destroy(this.gameObject, lifetime);
		}
	}
}
