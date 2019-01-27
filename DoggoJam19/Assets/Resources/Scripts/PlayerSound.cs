using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    public List<AudioClip> footSteps = new List<AudioClip>(8);
    private CharacterController myCC;
    private PlayerController myPC;
    private AudioSource myAS;
    private float delay = 0.0f;
    System.Random myRand;

    AudioClip barkClip;
    AudioSource barkSource;

    // Start is called before the first frame update
    void Start()
    {
        footSteps.Clear();
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Hardwood_1"));
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Hardwood_2"));
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Hardwood_3"));
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Hardwood_4"));
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Tile_1"));
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Tile_2"));
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Tile_3"));
        footSteps.Add(Resources.Load<AudioClip>("Audio/SFX/Footstep_Tile_4"));

        barkClip = Resources.Load<AudioClip>("Audio/Dog_Bark_2");


        myCC = GetComponent<CharacterController>();
        myAS = GetComponent<AudioSource>();
        myPC = GetComponent<PlayerController>();

        barkSource = gameObject.AddComponent<AudioSource>();
        barkSource.volume = 1.0f;
        barkSource.loop = false;

        myRand = new System.Random();
    }

    float timer = 1.0f;
    // Update is called once per frame
    void Update()
    {
        if (delay <= 0)
        {
            delay = Time.deltaTime * myRand.Next(8, 16);

            if (myCC.isGrounded && myPC.GetHorizontalVelocity() != Vector3.zero)
                myAS.PlayOneShot(footSteps[myRand.Next(0, 7)]);
        }


        if (timer > 1.0f)
        {
            if (Input.GetButtonDown("Bark") || Input.GetMouseButtonDown(0))
            {
                timer = 0.0f;
                barkSource.PlayOneShot(barkClip);
            }
        }
        else
            timer += Time.deltaTime;


        delay -= Time.deltaTime;
    }
}
