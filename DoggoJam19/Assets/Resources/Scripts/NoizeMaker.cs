using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoizeMaker : MonoBehaviour
{
    public GameObject noizeIndicator;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private GameObject player;
    private GameObject getIndicator;

    public bool makingNoize;
    private bool haveAnIndicator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        makingNoize = false;
        haveAnIndicator = false;
        getIndicator = null;
    }

    private void Update()
    {
        if (makingNoize && !haveAnIndicator)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x + offsetX,
                transform.position.y + offsetY, transform.position.z + offsetZ);
            GameObject temp;
            temp = Instantiate(noizeIndicator);
            getIndicator = temp;
            haveAnIndicator = true;
        }

        if(makingNoize && getIndicator)
            getIndicator.transform.LookAt(player.transform);
        else if (!makingNoize && getIndicator)
            DestroyObject(getIndicator);

    }

    private void DestroyObject(GameObject _toDestroy)
    {
        Destroy(_toDestroy, 0.1f);
        haveAnIndicator = false;
    }
}
