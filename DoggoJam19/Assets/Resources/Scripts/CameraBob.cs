using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraBob : MonoBehaviour
{
    public PlayerController m_PlayerController;

    public float m_SpeedMin = 0.5f;
    public float m_SpeedMax = 2.0f;
    public float m_HorizontalMagnitudeMin = 0.05f;
    public float m_VerticalMagnitudeMin = 0.10f;
    public float m_HorizontalMagnitudeMax = 0.15f;
    public float m_VerticalMagnitudeMax = 0.1f;

    Vector3 m_InitialPosition;

    // Start is called before the first frame update
    private void Awake()
    {
        Assert.IsNotNull(m_PlayerController);
    }

    void Start()
    {
        m_InitialPosition = transform.localPosition;
    }

    float phaseX = 0.0f;
    float phaseY = 0.0f;
    Vector3 cachedVel = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalVel = m_PlayerController.GetVelocity();
        horizontalVel.y = 0.0f;
        cachedVel = Vector3.MoveTowards(cachedVel, horizontalVel, 1.0f*Time.deltaTime);
        float vel = cachedVel.magnitude;
        float alphaVel = vel / m_PlayerController.m_MaxSpeedSprint;
        float magh = Mathf.Lerp(m_HorizontalMagnitudeMin, m_HorizontalMagnitudeMax, alphaVel);
        float magy = Mathf.Lerp(m_VerticalMagnitudeMin, m_VerticalMagnitudeMax, alphaVel);

        float speed = Mathf.Lerp(m_SpeedMin, m_SpeedMax, alphaVel);
        phaseX += speed * Time.deltaTime / 2.0f;
        phaseY += speed * Time.deltaTime;
        if (phaseX > Mathf.PI * 2)
            phaseX = 0.0f;

        if (phaseY > Mathf.PI * 2)
            phaseY = 0.0f;

        float alphaH = (Mathf.Cos(phaseX) + 1.0f) / 2.0f;
        float alphaV = (Mathf.Sin(phaseY) + 1.0f) / 2.0f;

        Vector3 bobOffset = Vector3.zero;
        bobOffset.x = Mathf.Lerp(-magh, +magh, alphaH);
        bobOffset.y = Mathf.Lerp(-magy, +magy, alphaV);

        transform.localPosition = m_InitialPosition + bobOffset;
    }
}
