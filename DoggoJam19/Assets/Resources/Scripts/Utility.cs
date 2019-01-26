using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public delegate void ActivateStartDelegate();
    public delegate void ActivateFinishDelegate();
    [SerializeField]
    static Vector3 GlobalOffset = new Vector3(0,0,2);
    public static Vector3 GetOffset()
    {
        return GlobalOffset;
    }

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
