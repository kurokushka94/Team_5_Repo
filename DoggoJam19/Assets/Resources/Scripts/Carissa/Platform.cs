using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Vector3 offset;

    Vector3 position;
    Vector3 newPosition;
    Time time;

    AudioSource source;
    public AudioClip upServoStart;
    public AudioClip upServoEnd;
    public AudioClip upServoLoop;
    public AudioClip downServoStart;
    public AudioClip downServoEnd;
    public AudioClip downServoLoop;

    private AudioClip startToPlay;
    private AudioClip endToPlay;
    private AudioClip loopToPlay;

    private float increase;
    private bool isMoving;
    private bool isPlaying;

    [HideInInspector] public bool isIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        source = GetComponent<AudioSource>();
        startToPlay = upServoStart;
        endToPlay = upServoEnd;
        loopToPlay = upServoLoop;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIncreasing)
        {
            increase = 0.01f;           
        }
        else if (!isIncreasing)
        {
            increase = -0.01f;
        }

        newPosition = position + offset;

        if (Vector3.Distance(transform.position, newPosition) > 0.01f && !isMoving)
        {
            StartCoroutine("MovePlatform");
            //source.Play();
        }
    }

    IEnumerator MovePlatform()
    {
        isMoving = true;
        int delay = 0;

        source.PlayOneShot(startToPlay);
        yield return new WaitForSecondsRealtime(0.4f);
        while(Vector3.Distance(transform.position, newPosition) >= 0.01f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + increase, transform.position.z);
            ++delay;
            source.clip = loopToPlay;
            if(delay % 25 == 0)
                source.Play();
            yield return new WaitForSecondsRealtime(0.01f);

        }
        yield return new WaitForSecondsRealtime(0.05f);
        source.Stop();
        source.PlayOneShot(endToPlay);
        isMoving = false;
    }

    void AddToOffset(Vector3 _distanceToAdd)
    {
        offset += _distanceToAdd;
    }

    void SubtractFromOffset(Vector3 _distanceToSubtract)
    {
        offset -= _distanceToSubtract;
    }
}
