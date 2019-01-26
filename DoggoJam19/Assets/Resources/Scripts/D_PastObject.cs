using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PastObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		D_EventManager.TriggerTest(collision, D_EventManager.CollisionTypes.Enter);
	}

	private void OnCollisionStay(Collision collision)
	{
		D_EventManager.TriggerTest(collision, D_EventManager.CollisionTypes.Stay);
	}
}
