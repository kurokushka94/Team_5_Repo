using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivePlayer : MonoBehaviour
{
	public GameObject FuturePlayer;
	public GameObject PastPlayer;
    [SerializeField]
    GameObject FadeScreen = null;
    [SerializeField]
    GameObject PastGhost = null;
    [SerializeField]
    GameObject FutureGhost = null;

	private Camera MainCam;
	private AudioSource myAS;
	private AudioClip pastMusic;
	private AudioClip futureMusic;
	//private CharacterController FutureController;
	//private CharacterController PastController;
	public bool PastIsActive = true;
	private bool TimeTraveling = false;

    // Start is called before the first frame update
    void Start()
    {
		MainCam = Camera.main;
		myAS = Camera.main.GetComponent<AudioSource>();
		pastMusic = Resources.Load<AudioClip>("Audio/SFX/Past_Loop_1");
		futureMusic = Resources.Load<AudioClip>("Audio/SFX/Present_Loop_1");


		FuturePlayer.GetComponent<CharacterController>().enabled = false;
		FuturePlayer.GetComponent<PlayerController>().enabled = false;
		GetComponent<CameraBob>().m_PlayerController = PastPlayer.GetComponent<PlayerController>();

		this.transform.SetParent(PastPlayer.transform, false);
		this.transform.SetAsFirstSibling();
		this.transform.position = PastPlayer.transform.position;
		this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

		PastPlayer.GetComponent<CharacterController>().enabled = true;
		PastPlayer.GetComponent<PlayerController>().enabled = true;

		myAS.loop = true;
		myAS.clip = pastMusic;
		myAS.Play();
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
            if (io != null)
                io.gameObject.SendMessage("DetachFromPlayer", SendMessageOptions.DontRequireReceiver);

			this.transform.DetachChildren();
		}
        FadeScreen.transform.SetParent(this.transform);

		oldPlayer.GetComponent<CharacterController>().enabled = false;
		oldPlayer.GetComponent<PlayerController>().enabled = false;
		GetComponent<CameraBob>().m_PlayerController = newPlayer.GetComponent<PlayerController>();

		yield return new WaitForEndOfFrame();

		float tempFOV = MainCam.fieldOfView;

        if(FadeScreen!= null)
            FadeScreen.GetComponent<FadeOut>().FadeOutScreen();

		float volume = myAS.volume;
		float STOREDvol = volume / 60.0f;
        for (float i = tempFOV; i < 120.0f; i += 1.0f)
		{
			MainCam.fieldOfView += 1.0f;
			myAS.volume = Mathf.Lerp(myAS.volume, 0.1f, 0.07f);
			yield return new WaitForEndOfFrame();
		}

		this.transform.SetParent(newPlayer.transform, false);
		this.transform.SetAsFirstSibling();
		this.transform.position = newPlayer.transform.position;
		this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

		myAS.Stop();
		if (PastIsActive) myAS.clip = pastMusic;
		else myAS.clip = futureMusic;
		myAS.Play();

		yield return new WaitForEndOfFrame();

		for(float i = 120.0f; i > tempFOV; i -= 1.0f)
		{
			myAS.volume = Mathf.Lerp(myAS.volume, volume, 0.05f);
			MainCam.fieldOfView -= 1.0f;
			yield return new WaitForEndOfFrame();
		}
		myAS.volume = volume;
		MainCam.fieldOfView = tempFOV;

		newPlayer.GetComponent<CharacterController>().enabled = true;
		newPlayer.GetComponent<PlayerController>().enabled = true;
		TimeTraveling = false;
        Debug.Log("Current: "+ FuturePlayer.transform.localPosition + " Next: "+ (FuturePlayer.transform.localPosition - Utility.GetOffset()));
        FutureGhost.transform.position = FuturePlayer.transform.position - Utility.GetOffset();
        Debug.Log("Current: " + PastPlayer.transform.localPosition + " Next: " + (PastPlayer.transform.localPosition - Utility.GetOffset()));
        PastGhost.transform.position = PastPlayer.transform.position + Utility.GetOffset();

    }
}
