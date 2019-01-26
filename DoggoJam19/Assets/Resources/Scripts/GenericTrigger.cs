using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTrigger : BaseCondition
{
	protected virtual void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == TargetName || other.gameObject.tag == TargetTag)
		{
			IsActivated = true;
		}
	}

	protected virtual void OnCollisionExit(Collision other)
	{
		if (other.gameObject.name == TargetName || other.gameObject.tag == TargetTag)
		{
			IsActivated = false;
		}
	}
}
