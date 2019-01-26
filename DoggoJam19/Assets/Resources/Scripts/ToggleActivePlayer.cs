using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivePlayer : MonoBehaviour
{
	public GameObject FuturePlayer;
	public GameObject PastPlayer;

	private Camera MainCam;
	//private CharacterController FutureController;
	//private CharacterController PastController;
	private bool PastIsActive = true;
	private bool TimeTraveling = false;

    // Start is called before the first frame update
    void Start()
    {
		MainCam = Camera.main;
		FuturePlayer.GetComponent<CharacterController>().enabled = false;
		FuturePlayer.GetComponent<PlayerController>().enabled = false;
		GetComponent<CameraBob>().m_PlayerController = PastPlayer.GetComponent<PlayerController>();

		this.transform.SetParent(PastPlayer.transform, false);
		this.transform.SetAsFirstSibling();
		this.transform.position = PastPlayer.transform.position;
		this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

		PastPlayer.GetComponent<CharacterController>().enabled = true;
		PastPlayer.GetComponent<PlayerController>().enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("TimeTravel"))
		{
			if(PastIsActive && !TimeTraveling) StartCoroutine(SwapPlayers(FuturePlayer, PastPlayer));
			else if (!TimeTraveling) StartCoroutine(SwapPlayers(PastPlayer, FuturePlayer));

			PastIsActive = !PastIsActive;
		}
    }

	private IEnumerator SwapPlayers(GameObject newPlayer, GameObject oldPlayer)
	{
		TimeTraveling = true;
		if(transform.childCount != 0)
		{
			InteractiveObject io = transform.GetComponentInChildren<InteractiveObject>();
			io.gameObject.SendMessage("DetachFromPlayer", SendMessageOptions.DontRequireReceiver);

			this.transform.DetachChildren();
		}
		
		oldPlayer.GetComponent<CharacterController>().enabled = false;
		oldPlayer.GetComponent<PlayerController>().enabled = false;
		GetComponent<CameraBob>().m_PlayerController = newPlayer.GetComponent<PlayerController>();

		yield return new WaitForEndOfFrame();

		float tempFOV = MainCam.fieldOfView;

		for(float i = tempFOV; i < 150.0f; i += 1.0f)
		{
			MainCam.fieldOfView += 1.0f;
			yield return new WaitForEndOfFrame();
		}

		this.transform.SetParent(newPlayer.transform, false);
		this.transform.SetAsFirstSibling();
		this.transform.position = newPlayer.transform.position;
		this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

		yield return new WaitForEndOfFrame();

		for(float i = 150.0f; i > tempFOV; i -= 1.0f)
		{
			MainCam.fieldOfView -= 1.0f;
			yield return new WaitForEndOfFrame();
		}

		MainCam.fieldOfView = tempFOV;

		newPlayer.GetComponent<CharacterController>().enabled = true;
		newPlayer.GetComponent<PlayerController>().enabled = true;
		TimeTraveling = false;
	}
}
