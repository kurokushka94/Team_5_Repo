using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        firstPasswordIn = false;
        secondPasswordIn = false;
        playGame = false;

        for (int i = 0; i < 4 * numRounds; ++i)
            simonsTurns.Add(rand.Next() % 4);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isActive)
        {
            bool playerIsInRange = (player.transform.position - transform.position).magnitude <= interactRange ? true : false;

            if (playerIsInRange)
            {
                RaycastHit hit;

                if (Physics.Raycast(player.transform.position, player.transform.GetChild(0).transform.forward, out hit, 15.0f))
                {
                    //Debug.DrawRay(player.transform.position, player.transform.GetChild(0).transform.forward, Color.blue);

                    if (hit.transform.name == "CowPicture")
                    {
                        nodes[0].GetComponentInChildren<Canvas>().enabled = true;
                        nodes[1].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[2].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[3].GetComponentInChildren<Canvas>().enabled = false;
                    }
                    else if (hit.transform.name == "CatPicture")
                    {
                        nodes[0].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[1].GetComponentInChildren<Canvas>().enabled = true;
                        nodes[2].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[3].GetComponentInChildren<Canvas>().enabled = false;
                    }
                    else if (hit.transform.name == "PigPicture")
                    {
                        nodes[0].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[1].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[2].GetComponentInChildren<Canvas>().enabled = true;
                        nodes[3].GetComponentInChildren<Canvas>().enabled = false;
                    }
                    else if (hit.transform.name == "DogPicture")
                    {
                        nodes[0].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[1].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[2].GetComponentInChildren<Canvas>().enabled = false;
                        nodes[3].GetComponentInChildren<Canvas>().enabled = true;
                    }
                }

                if (Input.GetButtonDown("Interact"))
                {
                    if (!firstPasswordIn && !playGame && !secondPasswordIn)
                        EnterFirstPassword();
                    else if (firstPasswordIn && !playGame && !secondPasswordIn)
                        PlaySimonSays();
                    else if (firstPasswordIn && playGame && !secondPasswordIn)
                        EnterSecondPassword();
                }
            }
        }
    }

    private void EnterFirstPassword()
    {

    }

    private void PlaySimonSays()
    {

    }
    private void EnterSecondPassword()
    {

    }

    private void AddPlayersChoice(int _choise)
    {
        playersTurns.Add(_choise);
    }
}
