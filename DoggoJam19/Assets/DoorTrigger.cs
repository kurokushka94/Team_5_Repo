using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : GenericListener
{
    public string AnimationName;

    Animation animationToPlay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void AllTriggersOccured()
    {
        animationToPlay = Resources.Load<Animation>(AnimationName);
        animationToPlay.Play();
    }
}
