using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class InteractiveObject : MonoBehaviour
{
	private bool PlayerInRange = false;
	private bool PickedUp = false;

	[SerializeField]
	private GameObject player = null;

	private Rigidbody myRigidBody = null;
	private Canvas myCanvas = null;

	private void Awake()
	{
		myRigidBody = GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
	void Start()
    {
		myCanvas = transform.GetComponentInChildren<Canvas>();
		myCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Interact"))
		{
			if (PlayerInRange && !PickedUp)
			{
				Debug.Log("Picking Up Item");
				PickedUp = true;

				Vector3 toItem = Vector3.Normalize(transform.position - player.transform.GetChild(0).transform.position); //vector that points from the player to the item

				float DotResult = Vector3.Dot(player.transform.GetChild(0).transform.forward, toItem);
				Debug.Log("Dot Product Result: " + DotResult);
				
				if (DotResult > 0.95f)
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

		if (myCanvas.enabled && player != null)
		{
			myCanvas.transform.LookAt(player.transform.position);
			myCanvas.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			PlayerInRange = true;
			Debug.Log("PLAYER IN RANGE");
			player = other.gameObject;
			myCanvas.enabled = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			PlayerInRange = false;
			myCanvas.enabled = false;
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
