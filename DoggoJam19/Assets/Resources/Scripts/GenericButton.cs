using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericButton : BaseCondition
{
	private GameObject player;
	private bool PlayerInRange = false;
	private Canvas myCanvas;

	private void Start()
	{
		myCanvas = transform.GetComponentInChildren<Canvas>();
		myCanvas.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		float DotResult = 0;

		if (player != null)
		{
			Vector3 toItem = Vector3.Normalize(transform.position - Camera.main.transform.position); //vector that points from the player to the item
			DotResult = Vector3.Dot(player.transform.GetChild(0).transform.forward, toItem);
		}

		if (Input.GetButtonDown("Interact"))
		{
			if (PlayerInRange && DotResult > 0.95f)
			{
				IsActivated = !IsActivated;
			}
		}

		if (player != null)
		{
			if (PlayerInRange && DotResult > 0.95f)
			{
				myCanvas.enabled = true;
				//myCanvas.transform.LookAt(player.transform.position);
				//myCanvas.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);
			}
			else myCanvas.enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			PlayerInRange = true;
			player = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			PlayerInRange = false;
		}
	}
}
