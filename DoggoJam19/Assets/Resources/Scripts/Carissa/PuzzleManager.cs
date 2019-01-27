using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PuzzleManager
{
    public delegate void VoidWithNoParameters();

    public static event VoidWithNoParameters PuzzleA1;
    public static event VoidWithNoParameters PuzzleA2;
    public static event VoidWithNoParameters PuzzleB1;
    public static event VoidWithNoParameters PuzzleB2;
    public static event VoidWithNoParameters PuzzleC1;
    public static event VoidWithNoParameters PuzzleC2;

    public static void TriggerPuzzleA1()
    {
        if (PuzzleA1 != null)
        {
            PuzzleA1();
        }
    }

    public static void TriggerPuzzleA2()
    {
        if (PuzzleA2 != null)
        {
            PuzzleA2();
        }
    }

    public static void TriggerPuzzleB1()
    {
        if (PuzzleB1 != null)
        {
            PuzzleB1();
        }
    }

    public static void TriggerPuzzleB2()
    {
        if (PuzzleB2 != null)
        {
            PuzzleB2();
        }
    }
    
    public static void TriggerPuzzleC1()
    {
        if (PuzzleC1 != null)
        {
            PuzzleC1();
        }
    }

    public static void TriggerPuzzleC2()
    {
        if (PuzzleC2 != null)
        {
            PuzzleC2();
        }
    }

}
