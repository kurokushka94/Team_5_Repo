using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : GenericListener
{
    public Animator mAnimator;

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
    }
}
