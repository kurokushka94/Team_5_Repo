using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysPuzzle : MonoBehaviour
{
    public GameObject[] nodes; // Goes in order: 1.Cow, 2.Cat, 3.Pig, 4.Dog

    public bool isActive;

    public float interactRange;

    public int numRounds;

    private List<int> simonsTurns;
    private List<int> playersTurns;

    private GameObject player;
    private System.Random rand;

    private bool correctPattern;
    private bool firstPasswordIn;
    private bool secondPasswordIn;
    private bool playGame;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rand = new System.Random();
        simonsTurns = new List<int>();
        playersTurns = new List<int>();
        correctPattern = true;
        firstPasswordIn = false;
        secondPasswordIn = false;
        playGame = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isActive)
        {
            bool playerIsInRange = (player.transform.position - transform.position).magnitude <= interactRange ? true : false;

            if (playerIsInRange)
            {
                InitializeSimon();

                RaycastHit hit;

                if (Physics.Raycast(player.transform.position, player.transform.GetChild(0).transform.forward, out hit, 15.0f))
                {
                    //Debug.DrawRay(player.transform.position, player.transform.GetChild(0).transform.forward, Color.blue);

                    if (hit.transform.name == "CowPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 0)
                            {
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                                if (Input.GetButtonDown("Interact"))
                                    AddPlayersChoice(i);
                            }
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                    }
                    else if (hit.transform.name == "CatPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 1)
                            {
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                                if (Input.GetButtonDown("Interact"))
                                    AddPlayersChoice(i);
                            }
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                    }
                    else if (hit.transform.name == "PigPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 2)
                            {
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                                if (Input.GetButtonDown("Interact"))
                                    AddPlayersChoice(i);
                            }
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                    }
                    else if (hit.transform.name == "DogPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 3)
                            {
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                                if (Input.GetButtonDown("Interact"))
                                    AddPlayersChoice(i);
                            }
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                    }

                    //Check players input against Simon
                    for (int i = 0; i < playersTurns.Count; ++i)
                    {
                        if (simonsTurns[i] != playersTurns[i])
                        {
                            correctPattern = false;
                            playersTurns.Clear();
                            break;
                        }
                    }
                }
            }
        }
    }

    private void InitializeSimon()
    {
        if (!firstPasswordIn && !playGame && !secondPasswordIn)
            for (int i = 0; i < 4; ++i)
                simonsTurns.Add(rand.Next() % 4);
        else if (firstPasswordIn && !playGame && !secondPasswordIn)
            for (int i = 0; i < 4 * numRounds; ++i)
                simonsTurns.Add(rand.Next() % 4);
        else if (firstPasswordIn && playGame && !secondPasswordIn) ;
                //Get Player's Password
    }

    private void AddPlayersChoice(int _choise)
    {
        playersTurns.Add(_choise);
    }

    private void CheckInput(int _pInput)
    {
        if (!firstPasswordIn && !playGame && !secondPasswordIn)
            EnterFirstPassword(_pInput);
        else if (firstPasswordIn && !playGame && !secondPasswordIn)
            PlaySimonSays(_pInput);
        else if (firstPasswordIn && playGame && !secondPasswordIn)
            EnterSecondPassword(_pInput);
    }

    private void WrongInput()
    {

    }

    private void EnterFirstPassword(int _pInput)
    {

    }

    private void PlaySimonSays(int _pInput)
    {

    }
    private void EnterSecondPassword(int _pInput)
    {

    }
}
