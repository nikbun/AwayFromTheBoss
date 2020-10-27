using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : AbstractAnimator<Enum, Enum, DialogAnimator.Bools, Enum>
{
	public enum Bools
	{
		Important
	}

	public enum Trigers
	{
		Hide
	}
}
