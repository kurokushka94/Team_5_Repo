using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class D_EventManager 
{
	public enum CollisionTypes { Enter, Stay, Exit };

	//delegates
	public delegate void PastEvent(Collision collision, CollisionTypes type);

	public static event PastEvent TestPastEvent;

	public static void TriggerTest(Collision collision, CollisionTypes type)
	{
		if(TestPastEvent != null)
		{
			TestPastEvent(collision, type);
		}
	}
}

