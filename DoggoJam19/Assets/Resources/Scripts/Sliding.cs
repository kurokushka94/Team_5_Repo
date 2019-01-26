using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    bool MoveObject = false;
    float timer = 0;
    private void Awake()
    {
        gameObject.GetComponent<Future>().AddActivateStartFunction(ActivateSlide);
    }
    private void Update()
    {
        if (MoveObject)
        {
            transform.position += new Vector3(0.1f, 0, 0);
            if (timer < 0.5f)
                timer += Time.deltaTime;
            else
            {
                MoveObject = false;
                Past test = gameObject.GetComponent<Past>();
                if (test)
                    test.FinishUpdate(null);
            }
        }
    }

    public void ActivateSlide()
    {
        MoveObject = true;
        timer = 0;
        Debug.Log("Slilding");
    }
}
