using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Future : MonoBehaviour
{

    protected Utility.ActivateStartDelegate ActivateStart;

    

    public void AddActivateStartFunction(Utility.ActivateStartDelegate _startFunction)
    {
        ActivateStart += _startFunction;
    }

    public virtual void FinishUpdate(GameObject _targetObject)
    {
        if (_targetObject != null)
            gameObject.transform.position = _targetObject.transform.position + Utility.GetOffset();
    }

    public virtual void ActivateObject()
    {
        if (ActivateStart != null)
            ActivateStart();
    }

}
