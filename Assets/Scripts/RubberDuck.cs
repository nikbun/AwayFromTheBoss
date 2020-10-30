using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RubberDuck : MonoBehaviour, IUsable
{
	[SerializeField] private AudioClip rubberDuckSound;

	public UnityEvent UseDuckEvent;

	public void Use()
	{
		UseDuckEvent?.Invoke();
		Audio.Instance.PlaySound(rubberDuckSound, this.transform, true);
	}
}
