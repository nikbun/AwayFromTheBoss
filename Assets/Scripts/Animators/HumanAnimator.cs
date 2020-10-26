using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimator : AbstractAnimator<Enum, Enum, HumanAnimator.Bools, HumanAnimator.Trigers>
{
	public enum Bools
	{
		Walk
	}

	public enum Trigers
	{
		Sneeze,
		Death
	}

	/// <summary>
	/// Для привязки звука к событиям анимации
	/// </summary>
	/// <param name="clip"></param>
	public void PlaySound(AudioClip clip)
	{
		Audio.Instance.PlaySound(clip, transform);
	}

	public void PlaySoundRandomPitch(AudioClip clip)
	{
		Audio.Instance.PlaySound(clip, transform, true);
	}
}
