using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSwitch : BaseCondition
{
	protected virtual void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == TargetName || other.gameObject.tag == TargetTag)
		{
			IsActivated = !IsActivated;
		}
	}
}
