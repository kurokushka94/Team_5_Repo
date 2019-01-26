using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPuzzle : MonoBehaviour
{
    public bool hasCompletedPuzzleA1;
    public bool hasCompletedPuzzleA2;
    public bool hasCompletedPuzzleB1;
    public bool hasCompletedPuzzleB2;
    public bool hasCompletedPuzzleC1;
    public bool hasCompletedPuzzleC2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCompletedPuzzleA1)
        {
            PuzzleManager.TriggerPuzzleA1();
        }
        if (hasCompletedPuzzleA2)
        {
            PuzzleManager.TriggerPuzzleA2();
        }
        if (hasCompletedPuzzleB1)
        {
            PuzzleManager.TriggerPuzzleB1();
        }
        if (hasCompletedPuzzleB2)
        {
            PuzzleManager.TriggerPuzzleB2();
        }
        if (hasCompletedPuzzleC1)
        {
            PuzzleManager.TriggerPuzzleC1();
        }
        if (hasCompletedPuzzleC2)
        {
            PuzzleManager.TriggerPuzzleC2();
        }


    }
}
