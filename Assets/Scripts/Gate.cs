using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Gate : MonoBehaviour, IUsable
{
	public void Open()
	{
		gameObject.SetActive(false);
	}

	public void Use()
	{
		Open();
	}
}
