using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuFunctions : MonoBehaviour
{
	public GameObject Main;
	public GameObject Credits;
	public GameObject Options;

	private Canvas MainCanvas;
	private Canvas CreditsCanvas;
	private Canvas OptionsCanvas;

    // Start is called before the first frame update
    void Start()
    {
		MainCanvas = Main.GetComponent<Canvas>();
		CreditsCanvas = Credits.GetComponent<Canvas>();
		OptionsCanvas = Options.GetComponent<Canvas>();

		CreditsCanvas.enabled = false;
		OptionsCanvas.enabled = false;
		MainCanvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void LoadGame()
	{
		SceneManager.LoadScene("Dylan"); //replace with game scene later
	}

	public void ToOptions()
	{
		MainCanvas.enabled = false;
		CreditsCanvas.enabled = false;
		OptionsCanvas.enabled = true;
	}

	public void ToCredits()
	{
		MainCanvas.enabled = false;
		OptionsCanvas.enabled = false;
		CreditsCanvas.enabled = true;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void BackToMain()
	{
		OptionsCanvas.enabled = false;
		CreditsCanvas.enabled = false;
		MainCanvas.enabled = true;
	}
}
