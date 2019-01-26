using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class InteractiveObject : MonoBehaviour
{
	private bool PlayerInRange = false;
	private bool PickedUp = false;
	private GameObject player = null;	
	private Rigidbody myRigidBody = null;

	private void Awake()
	{
		myRigidBody = GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonUp("Interact"))
		{
			if (PlayerInRange && !PickedUp)
			{
				Debug.Log("Picking Up Item");
				PickedUp = true;

				Vector3 toItem = transform.position - player.transform.position; //vector that points from the player to the item
				float temp = Vector3.Dot(player.transform.forward, toItem);
				Debug.Log("Dot Product Result: " + temp);
				if (Vector3.Dot(player.transform.forward, toItem) > 0.7f)
				{
					AttachToPlayer();
				}
			}
			else if (PickedUp)
			{
				PickedUp = false;
				DetachFromPlayer();
			}
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			PlayerInRange = true;
			Debug.Log("PLAYER IN RANGE");
			player = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			PlayerInRange = false;
			Debug.Log("PLAYER OUT OF RANGE");
		}
	}

	private void AttachToPlayer()
	{
		myRigidBody.useGravity = false;
		myRigidBody.detectCollisions = false;
		
		this.transform.SetParent(player.transform.GetChild(0), true);
		Debug.Log("Attached to Player");

	}

	private void DetachFromPlayer()
	{
		myRigidBody.useGravity = true;
		myRigidBody.detectCollisions = true;
		this.transform.SetParent(null);
		Debug.Log("Detached from Player");
	}

}
