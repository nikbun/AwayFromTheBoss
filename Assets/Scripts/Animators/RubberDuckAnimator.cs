using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberDuckAnimator : AbstractAnimator<Enum, Enum, Enum, RubberDuckAnimator.Trigers>
{
	public enum Trigers
	{
		PeepOutLeft,
		PeepOutRight
	}
}
