using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_FutureObject : MonoBehaviour
{
	Rigidbody myRigidBody;

	private void Awake()
	{
		myRigidBody = GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

	private void OnEnable()
	{
		D_EventManager.TestPastEvent += PastEventResponse;
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
		Debug.Log("FUTURE-- Collision Enter Data Received");
		myRigidBody.velocity = Vector3.zero;
		myRigidBody.AddForce(collision.impulse, ForceMode.Impulse);

	}

	private void OnCollisionStay(Collision collision)
	{
		Debug.Log("FUTURE-- Collision Stay Data Received");
		myRigidBody.velocity = Vector3.zero;
		myRigidBody.AddForce(collision.impulse, ForceMode.Impulse);

	}

	private void OnCollisionExit(Collision collision)
	{
		Debug.Log("FUTURE-- Collision Exit Data Received");
		myRigidBody.velocity = Vector3.zero;
		myRigidBody.AddForce(collision.impulse, ForceMode.Impulse);

	}

	private void PastEventResponse(Collision collision, D_EventManager.CollisionTypes type)
	{
		switch (type)
		{
			case D_EventManager.CollisionTypes.Enter:
				OnCollisionEnter(collision);
				break;
			case D_EventManager.CollisionTypes.Stay:
				OnCollisionStay(collision);
				break;
			case D_EventManager.CollisionTypes.Exit:
				OnCollisionExit(collision);
				break;
			default:
				break;
		}
	}

}
