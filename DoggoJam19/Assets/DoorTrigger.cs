using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DoorTrigger : GenericListener
{
    public Animator mAnimator;

    public UnityEvent events;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void AllTriggersOccured()
    {
        mAnimator.SetBool("Open", true);

        if (events != null)
        {
            events.Invoke();
        }
    }


    public void A1()
    {
        PuzzleManager.TriggerPuzzleA1();
    }

    public void B1()
    {
        PuzzleManager.TriggerPuzzleB1();
    }
}
