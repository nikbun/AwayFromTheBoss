using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Класс для указания параметров аниматора через Enum <br/>
/// Каждый Enum содержит названия параметров(конкретного типа) аниматора<br/>
/// <i>Если не нужны переменные какого-либо типа, можно проставить тип "Enum" как заглушку.</i>
/// </summary>
/// <typeparam name="F"> Enum с параметрами аниматора типа "float"</typeparam>
/// <typeparam name="I"> Enum с параметрами аниматора типа "int"</typeparam>
/// <typeparam name="B"> Enum с параметрами аниматора типа "bool"</typeparam>
/// <typeparam name="T"> Enum с параметрами аниматора типа "trigger"</typeparam>
[RequireComponent(typeof(Animator))]
public abstract class AbstractAnimator<F, I, B, T> : MonoBehaviour where F : Enum where I : Enum where B : Enum where T : Enum 
{
	private Animator _animator;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		CheckParameters(typeof(F));
		CheckParameters(typeof(I));
		CheckParameters(typeof(B));
		CheckParameters(typeof(T));
	}

	public void SetParameter(F parameter, float value)
	{
		_animator.SetFloat(parameter.ToString(), value);
	}

	public void SetParameter(I parameter, int value)
	{
		_animator.SetInteger(parameter.ToString(), value);
	}

	public void SetParameter(B parameter, bool value)
	{
		_animator.SetBool(parameter.ToString(), value);
	}

	public void SetParameter(T parameter)
	{
		_animator.SetTrigger(parameter.ToString());
	}

	private void CheckParameters(Type enumType)
	{
		if (enumType != typeof(Enum))
		{
			foreach (var parameter in Enum.GetNames(enumType))
			{
				if (!_animator.parameters.Any(p => p.name == parameter))
				{
					Debug.LogError("В аниматоре отсутствует параметр - " + parameter);
				}
			}
		}
	}
}