﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closer : MonoBehaviour
{
	public void Close()
	{
		this.gameObject.SetActive(false);
	}
}
