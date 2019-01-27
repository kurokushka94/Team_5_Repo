using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmellyObject : MonoBehaviour
{
    //public Camera mainCamera;

    public GameObject player;
    public GameObject smellEffect;

    public float smellRange;

    public bool isSmelling;
    private bool isEffecting;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isSmelling && !isEffecting && Vector3.Distance(player.transform.position, transform.position) > smellRange)
        {
            StartCoroutine("EffectWait");
        }
    }

    private void OnEnable()
    {
        if (gameObject.name == "PuzzleA1")
        {
            PuzzleManager.PuzzleA1 += SetEffectOff;
        }
        else if (gameObject.name == "PuzzleA2")
        {
            PuzzleManager.PuzzleA2 += SetEffectOff;
            PuzzleManager.PuzzleA1 += SetEffectOn;
        }
        else if (gameObject.name == "PuzzleB1")
        {
            PuzzleManager.PuzzleB1 += SetEffectOff;
        }
        else if (gameObject.name == "PuzzleB2")
        {
            PuzzleManager.PuzzleB2 += SetEffectOff;
            PuzzleManager.PuzzleB1 += SetEffectOn;
        }
        else if (gameObject.name == "PuzzleC1")
        {
            PuzzleManager.PuzzleC1 += SetEffectOff;
        }
        else if (gameObject.name == "PuzzleC2")
        {
            PuzzleManager.PuzzleC2 += SetEffectOff;
            PuzzleManager.PuzzleC1 += SetEffectOn;
        }
    }

    private void OnDisable()
    {
        if (gameObject.name == "PuzzleA1")
        {
            PuzzleManager.PuzzleA1 -= SetEffectOff;
        }
        else if (gameObject.name == "PuzzleA2")
        {
            PuzzleManager.PuzzleA2 -= SetEffectOff;
            PuzzleManager.PuzzleA1 -= SetEffectOn;
        }
        else if (gameObject.name == "PuzzleB1")
        {
            PuzzleManager.PuzzleB1 -= SetEffectOff;
        }
        else if (gameObject.name == "PuzzleB2")
        {
            PuzzleManager.PuzzleB2 -= SetEffectOff;
            PuzzleManager.PuzzleB1 -= SetEffectOn;
        }
        else if (gameObject.name == "PuzzleC1")
        {
            PuzzleManager.PuzzleC1 -= SetEffectOff;
        }
        else if (gameObject.name == "PuzzleC2")
        {
            PuzzleManager.PuzzleC2 -= SetEffectOff;
            PuzzleManager.PuzzleC1 -= SetEffectOn;
        }
    }

    IEnumerator EffectWait()
    {
        isEffecting = true;

        Vector3 offset = new Vector3(0f, -1f, 1f);
        NavMeshHit target;
        NavMesh.SamplePosition(player.transform.position, out target, 2.0f, NavMesh.AllAreas);

        if (target.hit)
        {
            GameObject temp = Instantiate(smellEffect, target.position, player.transform.rotation);
            temp.SendMessage("SetTarget", gameObject);

            yield return new WaitForSecondsRealtime(0.15f);

        }
        isEffecting = false;
    }

    void SetEffectOff()
    {
        isSmelling = false;
    }

    void SetEffectOn()
    {
        isSmelling = true;
    }
}
