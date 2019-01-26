using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Past : Future
{
    [SerializeField]
    GameObject futureObject = null;
    public override void ActivateObject()
    {
        ActivateStart();
    //    futureObject.GetComponent<Future>().ActivateObject(this.gameObject);
    }
    public override void FinishUpdate(GameObject _pointLess)
    {
       futureObject.GetComponent<Future>().FinishUpdate(this.gameObject);
    }
}
