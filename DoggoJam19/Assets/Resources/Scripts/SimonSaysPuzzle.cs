using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysPuzzle : MonoBehaviour
{
    public GameObject[] nodes; // Goes in order: 1.Cow, 2.Cat, 3.Pig, 4.Dog
    public GameObject   FutureWall;
    public GameObject   FirstWall;
    public Canvas       wrongInput;

    [HideInInspector]   public List<int> simonsTurns;
    [HideInInspector]   public bool inputWasSaved;
    public bool         isActive;
    public bool         saveInput;
    public bool         futureSimon;
    public bool         firstWall;
    public float        interactRange;
    public int          numRounds;

    private List<int>               playersTurns;
    private SimonSaysPuzzle[]       allSimons;

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
    private bool finishedInput;
    //private bool waiting;
    //private bool collectedInput;

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
        inputWasSaved =         false;
        finishedInput =         false;
        //waiting =               false;
        //collectedInput =        false;
        initialyActive = isActive == true ? true : false;

        //Debug.Log("FUTURE SIMON IS: " + futureSimon);

        currRound = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!simonIsInitialized)
        {
            if (!isActive && !saveInput && !initialyActive && !futureSimon)
            {
                InitializeSimon();

                Sprite[] switchPictures = new Sprite[4];
                switchPictures[0] = Resources.Load<Sprite>("Sprites\\Cow");
                switchPictures[1] = Resources.Load<Sprite>("Sprites\\Cat");
                switchPictures[2] = Resources.Load<Sprite>("Sprites\\Pig");
                switchPictures[3] = Resources.Load<Sprite>("Sprites\\Dog");

                for (int i = 0; i < 4; ++i)
                    nodes[i].GetComponentInChildren<SpriteRenderer>().sprite = switchPictures[simonsTurns[i]];

                if (FirstWall)
                    FirstWall.GetComponent<SimonSaysPuzzle>().simonsTurns = this.simonsTurns;

            }
        }
        bool playerIsInRange = (player.transform.position - transform.position).magnitude <= interactRange ? true : false;

        if (isActive)
        {
            if (playerIsInRange)
            {
                if (!simonIsInitialized && !saveInput &&initialyActive &&!inputWasSaved)
                    InitializeSimon();

                RaycastHit hit;

                if (Physics.Raycast(player.transform.position, Camera.main.transform.forward, out hit, 10.0f))
                {
                    //Debug.DrawRay(player.transform.position, player.transform.GetChild(0).transform.forward, Color.blue);

                    if (currRound <= numRounds && !simonFinishedSaying && !simonIsSaying && !saveInput && !inputWasSaved && !firstWall)
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
                            //CheckInput(0, currRound - 1);
                            //++currRound;
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
                            //CheckInput(1, currRound - 1);
                            //++currRound;
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
                            //CheckInput(2, currRound - 1);
                            //++currRound;
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
                            //CheckInput(3, currRound - 1);
                            //++currRound;
                        }
                    }

                    if (!saveInput && !simonIsSaying && havePInput && !futureSimon && !firstWall)
                    {
                        //Check players input against Simon
                        for (int i = 0; i < playersTurns.Count; ++i)
                        {
                            if (playersTurns.Count == currRound)
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

                        if (playersTurns.Count == currRound)
                            finishedInput = true;

                        if (madeMistake)
                        {
                            currRound = 1;
                            finishedInput = true;
                        }

                        if (finishedInput)
                        {
                            simonFinishedSaying = false;
                            havePInput = false;
                            playersTurns.Clear();
                            if (!madeMistake)
                                ++currRound;
                            madeMistake = false;
                            finishedInput = false;
                        }

                        if (correctInput == true)
                        {
                            isActive = false;
                            for (int i = 0; i < 4; ++i)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;

                            if (!firstPasswordIn && !playedGame && !secondPasswordIn)
                                for (int i = 0; i < allSimons.Length; ++i)
                                    allSimons[i].SendMessage("SetFlag", firstPasswordIn, SendMessageOptions.DontRequireReceiver);
                            else if (firstPasswordIn && !playedGame && !secondPasswordIn)
                                for (int i = 0; i < allSimons.Length; ++i)
                                    allSimons[i].SendMessage("SetFlag", playedGame, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                    else if (saveInput && !simonIsSaying && havePInput)
                    {
                        if (playersTurns.Count == 5)
                        {
                            isActive = false;
                            for (int i = 0; i < allSimons.Length; ++i)
                            {
                                allSimons[i].SendMessage("SetFlag", secondPasswordIn, SendMessageOptions.DontRequireReceiver);
                            }
                            for (int i = 0; i < 4; ++i)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;

                            FutureWall.GetComponent<SimonSaysPuzzle>().inputWasSaved = true;
                            FutureWall.GetComponent<SimonSaysPuzzle>().simonsTurns.Clear();
                            FutureWall.GetComponent<SimonSaysPuzzle>().simonsTurns = playersTurns;
                        }

                        havePInput = false;
                    }
                    if((futureSimon && havePInput) || (firstWall && havePInput))
                    {
                        for (int i = 0; i < playersTurns.Count; ++i)
                        {
                            if (playersTurns.Count == simonsTurns.Count)
                                correctInput = true;

                            if (playersTurns[i] != simonsTurns[i])
                            {
                                correctInput = false;
                                madeMistake = true;
                                break;
                            }
                        }
                       
                        if (madeMistake)
                        {
                            StartCoroutine("WrongInput");
                            currRound = 1;
                            finishedInput = true;
                        }

                        if (playersTurns.Count == simonsTurns.Count)
                            finishedInput = true;

                        if (finishedInput)
                        {
                            playersTurns.Clear();
                            if (!madeMistake)
                                ++currRound;
                            madeMistake = false;
                            finishedInput = false;
                            havePInput = false;
                        }

                        if(correctInput)
                        {
                            isActive = false;
                            for (int i = 0; i < 4; ++i)
                                nodes[i].GetComponentInChildren<Canvas>().enabled = false;
                        }
                    }
                    //collectedInput = false;
                }
            }
        }

        if(!playerIsInRange && initialyActive && !saveInput && !futureSimon)
        {
            currRound = 1;
            playersTurns.Clear();
            correctInput = false;
            isActive = true;
            simonIsInitialized = false;
        }
    }

    private void CheckInput(int _input, int _index)
    {
        if (playersTurns.Count == currRound)
            if (playersTurns.Count == simonsTurns.Count)
                correctInput = true;

        if (playersTurns[_index] != simonsTurns[_index])
        {
            if (!wrongInputShow)
                StartCoroutine("WrongInput");
            madeMistake = true;
        }

    }

    private void InitializeSimon()
    {
        if (!firstPasswordIn && !playedGame && !secondPasswordIn && !saveInput && !firstWall && !isActive)
        {
            for (int i = 0; i < 4; ++i)
                simonsTurns.Add(rand.Next() % 4);
        }

        else if (!saveInput && !firstWall && !futureSimon && isActive)
            for (int i = 0; i < numRounds; ++i)
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

    void SetFlag(bool _toSet)
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

    //IEnumerator WaitForInput(float _seconds)
    //{
    //    waiting = true;
    //    yield return new WaitForSecondsRealtime(_seconds);
    //    collectedInput = true;
    //    waiting = false;
    //}
}
