﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public delegate void ActivateStartDelegate();
    public delegate void ActivateFinishDelegate();
    [SerializeField]
    static Vector3 GlobalOffset = new Vector3(2,0,0);
    public static Vector3 GetOffset()
    {
        return GlobalOffset;
    }

<<<<<<< HEAD
    public static bool IsFuture(GameObject _target)
    {
        bool FutureTest = (_target.GetComponent<Future>() != null);
        bool PastTest = (_target.GetComponent<Past>() != null);

        if ((!PastTest) && (FutureTest))
            return FutureTest;

        return false;
    }



=======
	public static bool PlayerHasAnItem = false;
>>>>>>> 01b4904cbcf0cb7afc7bc31983ac3dc031e9d333
    //enum SCRIPTINDEX
    //{
    //    Sliding = 0
    //};


    //ActivateDelegate GetActivateScript(int _scriptIndex, GameObject _target)
    //{
    //    switch ((SCRIPTINDEX)(_scriptIndex))
    //    {
    //        case SCRIPTINDEX.Sliding:
    //            _target.AddComponent<Sliding>();
    //            return _target.GetComponent<Sliding>().ActivateSlide;
    //        default:
    //            break;
    //    }
    //    return null;
    //}



}
