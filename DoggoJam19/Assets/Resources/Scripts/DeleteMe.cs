using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMe : MonoBehaviour
{
    [SerializeField]
    bool future = false;
    [SerializeField]
    bool past = false;
    [SerializeField]
    GameObject FutureObject;
    [SerializeField]
    GameObject PastObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (future)
        {
            FutureObject.GetComponent<Future>().ActivateObject();
            future = false;
        }
        if (past)
        {
            PastObject.GetComponent<Past>().ActivateObject();
            past = false;
        }
    }
}
