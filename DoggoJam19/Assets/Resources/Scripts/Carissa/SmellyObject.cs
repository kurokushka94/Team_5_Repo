using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellyObject : MonoBehaviour
{
    //public Camera mainCamera;
    GameObject player;
    public GameObject smellEffect;

    public bool isSmelling;
    private bool isEffecting;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isSmelling && !isEffecting)
        {
            StartCoroutine("EffectWait");
        }
    }

    IEnumerator EffectWait()
    {
        isEffecting = true;

        Vector3 offset = new Vector3(0f, -5f, 1f);
        GameObject temp = Instantiate(smellEffect, player.transform.position + offset, player.transform.rotation);
        temp.SendMessage("SetTarget", gameObject);

        yield return new WaitForSecondsRealtime(0.15f);

        isEffecting = false;
    }
}
