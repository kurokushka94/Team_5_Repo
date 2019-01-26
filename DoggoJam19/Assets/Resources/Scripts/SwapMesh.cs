using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMesh : MonoBehaviour
{
    [SerializeField]
    bool future = false;
    [SerializeField]
    bool past = false;
    [SerializeField]
    Mesh NewMesh = null;

    private void Awake()
    {
        gameObject.GetComponent<Future>().AddActivateStartFunction(SetNewMesh);
    }

    void SetNewMesh()
    {
        Past testingPast = gameObject.GetComponent<Past>();
        Future testingFuture = gameObject.GetComponent<Future>();


        if (future)
            if(Utility.IsFuture(gameObject))
                testingFuture.GetComponent<MeshFilter>().mesh = NewMesh;

        if (past)
            if (!Utility.IsFuture(gameObject))
                testingPast.GetComponent<MeshFilter>().mesh = NewMesh;
    }
}
