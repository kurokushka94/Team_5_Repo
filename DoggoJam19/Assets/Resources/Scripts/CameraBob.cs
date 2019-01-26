using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraBob : MonoBehaviour
{
    public PlayerController m_PlayerController;

    public float m_Speed = 2.0f;
    public float m_HorizontalMagnitude = 0.2f;
    public float m_VerticalMagnitude = 0.2f;
    public float m_RestitutionSpeed = 1.0f;

    float m_Phase = 0.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        Assert.IsNotNull(m_PlayerController);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalVel = m_PlayerController.GetVelocity();
        horizontalVel.y = 0.0f;

        m_Phase += horizontalVel.magnitude * m_Speed;

        if (m_Phase > Mathf.PI * 2)
        {
            m_Phase = 0.0f;
        }

        float alpha = (Mathf.Sin(m_Phase)+1.0f)/2.0f;

        float targetX = Mathf.Sin(m_Phase);
        float targetY = Mathf.Cos(m_Phase);
    }
}
