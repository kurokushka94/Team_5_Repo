using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericButton : BaseCondition
{
	private GameObject player;
	private bool PlayerInRange = false;
	private Canvas myCanvas;
	private bool AlreadyActivated = false;
	private Light dirLight;

	private void Start()
	{
		myCanvas = transform.GetComponentInChildren<Canvas>();
		myCanvas.enabled = false;
		dirLight = GameObject.FindGameObjectWithTag("DirLight").GetComponent<Light>();
		dirLight.transform.eulerAngles = new Vector3(15, 5, 0);
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
				if(!AlreadyActivated)
				{
					AlreadyActivated = true;
					StartCoroutine(IControlTheSun());
				}
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

	private IEnumerator IControlTheSun()
	{
		float angle = dirLight.transform.eulerAngles.x;
		float origang = angle;

		while (angle < origang + 50.0f)
		{
			angle = Mathf.LerpAngle(angle, origang + 50.0f, Time.deltaTime * 0.1f);

			dirLight.transform.eulerAngles = new Vector3(angle, 5.0f, 0.0f);
			yield return new WaitForEndOfFrame();
		}
	}
}
