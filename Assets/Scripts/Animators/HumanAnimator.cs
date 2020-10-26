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
}
