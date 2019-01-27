using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    float FadeTimer = 0;
    [SerializeField]
    float SpacerDelay = 0;
    bool isFadingOut = false;
    bool isFadingIn = false;
    [SerializeField]
    float Speed = 0.1f;
    [SerializeField]
    Material targetMaterial = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (targetMaterial == null)
        {
            Renderer test = gameObject.GetComponent<Renderer>();
            if (test)
            {
                targetMaterial = test.material;
                targetMaterial.SetFloat("_Disperse", FadeTimer);
            }
        }
    }

    // 1 = transparent
    // 0 = black
    void Update()
    {
        if(isFadingIn)
        {
            FadeTimer -= Time.deltaTime* Speed;
            if (FadeTimer <= 0)
                isFadingIn = false;
        }
        if (isFadingOut)
        {
            FadeTimer += Time.deltaTime* Speed;
            if (FadeTimer >= 1+SpacerDelay)
            {
                isFadingOut = false;
                isFadingIn = true;
            }
        }
        if(isFadingIn||isFadingOut)
        {
            targetMaterial.SetFloat("_Disperse", FadeTimer);
        }
        else
        {
            FadeTimer = 0;
            targetMaterial.SetFloat("_Disperse", FadeTimer);
        }
    }
    //-1 => Left
    // 1 => Right
    public void FadeOutScreen(int _direction)
    {
        isFadingOut = true;
        targetMaterial.SetFloat("_Direction", _direction);
    }
}
