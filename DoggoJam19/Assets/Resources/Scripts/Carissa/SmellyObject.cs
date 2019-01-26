using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellyObject : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject smellEffect;

    public bool isSmelling;
    private bool isEffecting;

    // Start is called before the first frame update
    void Start()
    {
        
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

        Vector3 offset = new Vector3(0f, -1f, 1f);
        GameObject temp = Instantiate(smellEffect, mainCamera.transform.position + offset, mainCamera.transform.rotation);
        temp.SendMessage("SetTarget", gameObject);

        yield return new WaitForSecondsRealtime(0.1f);

        isEffecting = false;
    }
}
