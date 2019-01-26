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

    // Start is called before the first frame update
    void Start()
    {
		MainCam = Camera.main;
		SwapPlayers(PastPlayer, FuturePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("TimeTravel"))
		{
			if(PastIsActive) SwapPlayers(FuturePlayer, PastPlayer);
			else SwapPlayers(PastPlayer, FuturePlayer);

			PastIsActive = !PastIsActive;
		}
    }

	private void SwapPlayers(GameObject newPlayer, GameObject oldPlayer)
	{
		if(transform.childCount != 0)
		{
			InteractiveObject io = transform.GetComponentInChildren<InteractiveObject>();
			io.gameObject.SendMessage("DetachFromPlayer", SendMessageOptions.DontRequireReceiver);

			this.transform.DetachChildren();
		}

		this.transform.SetParent(newPlayer.transform, false);
		this.transform.SetAsFirstSibling();
		this.transform.position = newPlayer.transform.position;
		this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
		newPlayer.GetComponent<CharacterController>().enabled = true;
		newPlayer.GetComponent<PlayerController>().enabled = true;
		oldPlayer.GetComponent<CharacterController>().enabled = false;
		oldPlayer.GetComponent<PlayerController>().enabled = false;
		GetComponent<CameraBob>().m_PlayerController = newPlayer.GetComponent<PlayerController>();
	}
}
