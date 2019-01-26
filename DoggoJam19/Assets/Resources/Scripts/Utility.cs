using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public delegate void ActivateStartDelegate();
    public delegate void ActivateFinishDelegate();
    [SerializeField]
    static Vector3 GlobalOffset = new Vector3(0, 100, 0);
    public static bool PlayerHasAnItem = false;
    public static Vector3 GetOffset()
    {
        return GlobalOffset;
    }

    public static bool IsFuture(GameObject _target)
    {
        bool FutureTest = (_target.GetComponent<Future>() != null);
        bool PastTest = (_target.GetComponent<Past>() != null);

        if ((!PastTest) && (FutureTest))
            return FutureTest;

        return false;
    }

}
