using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWon : MonoBehaviour
{
    private int score;


    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score == 3)
        {
            transform.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }
    }

    void AddScore()
    {
        score += 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
