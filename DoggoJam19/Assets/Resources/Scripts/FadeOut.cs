using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    float FadeTimer = 0;
    bool isFadingOut = false;
    bool isFadingIn = false;
    Material targetMaterial = null;
    // Start is called before the first frame update
    private void Awake()
    {
        Renderer test = gameObject.GetComponent<Renderer>();
        if (test)
            targetMaterial = test.material;
    }

    // 1 = transparent
    // 0 = black
    void Update()
    {
        if(isFadingIn)
        {
            FadeTimer += Time.deltaTime;
            if (FadeTimer >= 1)
                isFadingIn = false;
        }
        if (isFadingOut)
        {
            FadeTimer -= Time.deltaTime;
            if (FadeTimer <= 0)
            {
                isFadingOut = false;
                isFadingIn = true;
            }
        }
        if(isFadingIn||isFadingOut)
        {
            targetMaterial.SetFloat("_Disperse", FadeTimer);
        }
    }
    //-1 => Left
    // 1 => Right
    public void FadeOutScreen()//int _direction)
    {
        isFadingOut = true;
        targetMaterial.SetFloat("_Direction", 1);
    }
}
