using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysPuzzle : MonoBehaviour
{
    public GameObject[] nodes; // Goes in order: 1.Cow, 2.Cat, 3.Pig, 4.Dog
    public Canvas       wrongInput;

    public bool     isActive;
    public bool     saveInput;
    public float    interactRange;
    public int      numRounds;

    private List<int>           simonsTurns;
    private List<int>           playersTurns;
    private SimonSaysPuzzle[]        allSimons;

    private GameObject          player;
    private System.Random       rand;

    private int currRound;

    private bool wrongInputShow;
    private bool correctInput;
    private bool madeMistake;
    private bool simonIsInitialized;
    private bool isLit;
    private bool simonIsSaying;
    private bool simonFinishedSaying;
    private bool havePInput;
    private bool firstPasswordIn;
    private bool secondPasswordIn;
    private bool playedGame;
    private bool initialyActive;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rand =          new System.Random();
        simonsTurns =   new List<int>();
        playersTurns =  new List<int>();
        allSimons = FindObjectsOfType<SimonSaysPuzzle>();

        wrongInputShow =        false;
        correctInput =          false;
        simonIsInitialized =    false;
        isLit =                 false;
        firstPasswordIn =       false;
        secondPasswordIn =      false;
        playedGame =            false;
        simonIsSaying =         false;
        simonFinishedSaying =   false;
        havePInput =            false;
        madeMistake =           false;
        initialyActive = isActive == true ? true : false;

        currRound = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        bool playerIsInRange = (player.transform.position - transform.position).magnitude <= interactRange ? true : false;

        if (isActive)
        {
            if (playerIsInRange)
            {
                if (!simonIsInitialized && !saveInput)
                    InitializeSimon();

                RaycastHit hit;

                if (Physics.Raycast(player.transform.position, player.transform.GetChild(0).transform.forward, out hit, 10.0f))
                {
                    //Debug.DrawRay(player.transform.position, player.transform.GetChild(0).transform.forward, Color.blue);

                    if (currRound <= numRounds && !simonFinishedSaying && !simonIsSaying && !saveInput)
                        StartCoroutine("SimonSays", currRound);

                    if (hit.transform.name == "CowPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 0)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                        if (Input.GetButtonDown("Interact"))
                        {
                            AddPlayersChoice(0);
                            StartCoroutine("LitPicture", 0);
                            havePInput = true;
                        }
                    }
                    else if (hit.transform.name == "CatPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 1)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                        if (Input.GetButtonDown("Interact"))
                        {
                            AddPlayersChoice(1);
                            StartCoroutine("LitPicture", 1);
                            havePInput = true;
                        }
                    }
                    else if (hit.transform.name == "PigPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 2)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                        if (Input.GetButtonDown("Interact"))
                        {
                            AddPlayersChoice(2);
                            StartCoroutine("LitPicture", 2);
                            havePInput = true;
                        }
                    }
                    else if (hit.transform.name == "DogPicture")
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (i == 3)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = true;
                            else
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                        if (Input.GetButtonDown("Interact"))
                        {
                            AddPlayersChoice(3);
                            StartCoroutine("LitPicture", 3);
                            havePInput = true;
                        }
                    }

                    if (!saveInput && !simonIsSaying && havePInput && playersTurns.Count == currRound)
                    {
                        //Check players input against Simon
                        for (int i = 0; i < playersTurns.Count; ++i)
                        {
                            if (playersTurns.Count == simonsTurns.Count)
                                correctInput = true;

                            if (playersTurns[i] != simonsTurns[i])
                            {
                                if (!wrongInputShow)
                                    StartCoroutine("WrongInput");
                                madeMistake = true;
                                break;
                            }
                        }

                        simonFinishedSaying = false;
                        havePInput = false;
                        playersTurns.Clear();

                        if (madeMistake)
                        {
                            currRound = 1;
                            madeMistake = false;
                        }
                        else
                        {
                            ++currRound;
                        }

                        if (correctInput == true)
                        {
                            isActive = false;
                            for (int i = 0; i < 4; ++i)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;

                            if (!firstPasswordIn && !playedGame && !secondPasswordIn)
                                for (int i = 0; i < allSimons.Length; ++i)
                                    allSimons[i].SendMessage("SetTrigger", firstPasswordIn, SendMessageOptions.DontRequireReceiver);
                            else if (firstPasswordIn && !playedGame && !secondPasswordIn)
                                for (int i = 0; i < allSimons.Length; ++i)
                                    allSimons[i].SendMessage("SetTrigger", playedGame, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                    else if (saveInput && !simonIsSaying && havePInput)
                    {
                        if (playersTurns.Count == 5)
                        {
                            isActive = false;
                            for (int i = 0; i < allSimons.Length; ++i)
                            {
                                allSimons[i].SendMessage("SetTrigger", secondPasswordIn, SendMessageOptions.DontRequireReceiver);
                                allSimons[i].SendMessage("SetSimonsChoice", playersTurns, SendMessageOptions.DontRequireReceiver);
                            }
                            for (int i = 0; i < 4; ++i)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }

                        havePInput = false;
                    }
                }
            }
        }
        else if(!isActive && !saveInput)
        {
            if (!simonIsInitialized)
                InitializeSimon();


        }

        if(!playerIsInRange && initialyActive)
        {
            currRound = 1;
            playersTurns.Clear();
            correctInput = false;
            isActive = true;
            simonIsInitialized = false;
        }
    }

    private void InitializeSimon()
    {
        if (!firstPasswordIn && !playedGame && !secondPasswordIn && !saveInput)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; j++)
                {
                    int random = rand.Next() % 4;
                    if (!simonsTurns.Contains(random))
                    {
                        simonsTurns.Add(random);
                        break;
                    }
                }
            }
        }
        else if (firstPasswordIn && !playedGame && !secondPasswordIn && !saveInput)
            for (int i = 0; i < 4 * numRounds; ++i)
                simonsTurns.Add(rand.Next() % 4);

        simonIsInitialized = true;
    }

    private void AddPlayersChoice(int _choise)
    {
        playersTurns.Add(_choise);
    }

    private void SetSimonsChoice(List<int> _playersPassword)
    {
        simonsTurns = _playersPassword;
    }

    void SetTrigger(bool _toSet)
    {
        _toSet = true;
    }

    IEnumerator WrongInput()
    {
        wrongInputShow = true;
        wrongInput.enabled = true;
        yield return new WaitForSecondsRealtime(1.0f);
        wrongInput.enabled = false;
        wrongInputShow = false;
    }

    IEnumerator LitPicture(int _index)
    {
        isLit = true;
        nodes[_index].GetComponentInChildren<Light>().enabled = true;

        //for (int i = 0; i < simonsTurns.Count; ++i)
        //    Debug.Log("Simons choise:\t" + simonsTurns[i] + '\t');
        //Debug.Log("\nLITTING " + nodes[_index].name + " LIGHT\n");

        yield return new WaitForSecondsRealtime(0.3f);
        nodes[_index].GetComponentInChildren<Light>().enabled = false;
        isLit = false;
    }

    IEnumerator SimonSays(int _currRound)
    {
        simonIsSaying = true;
        yield return new WaitForSecondsRealtime(1.0f);
        for (int i = 0; i < _currRound; ++i)
        {
            if (!isLit)
                StartCoroutine("LitPicture", simonsTurns[i]);
            yield return new WaitForSecondsRealtime(0.5f);
        }

        simonFinishedSaying = true;
        simonIsSaying = false;
    }
}
