using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class JammedDoor : MonoBehaviour
{
    public float m_Cooldown = 1.0f;
    public int m_Health = 4;
    float initialHealth;
    HingeJoint hinge;
    Vector3 hitPos;
    bool onCooldown = false;
    GameObject other;
    bool track = false;
    public void TakeDamage(GameObject instigator)
    {
        hitPos = instigator.transform.position;
        other = instigator;
        if (m_Health > 0)
        {
            if (onCooldown || track)
                return;
            onCooldown = true;
            StartCoroutine(DamageTimer());
            m_Health--;
            JointLimits newLimit = hinge.limits;
            newLimit.max = Mathf.Lerp(20.0f, 0.0f, m_Health / initialHealth);
            hinge.limits = newLimit;
            track = true;
        }
        else
        {
            JointLimits newLimit = hinge.limits;
            newLimit.max = 140.0f;
            hinge.limits = newLimit;

            JointSpring spring = hinge.spring;
            spring.spring = 0.0f;
            hinge.spring = spring;

            enabled = false;
        }
    }

    IEnumerator DamageTimer()
    {
        yield return new WaitForSeconds(m_Cooldown);
        onCooldown = false;
    }

    private void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        initialHealth = m_Health;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float m_Distance = 0.5f;
    void Update()
    {
        if(track)
        {
            if (Vector3.Distance(hitPos, other.transform.position) > m_Distance)
                track = false;
        }
    }
}
