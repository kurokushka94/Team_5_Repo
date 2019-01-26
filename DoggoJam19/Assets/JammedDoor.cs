using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class JammedDoor : MonoBehaviour
{
    public float m_Cooldown = 1.0f;
    public float m_Health = 100.0f;
    float initialHealth;
    HingeJoint hinge;

    bool onCooldown = false;
    public void TakeDamage(float damage)
    {
        if (onCooldown)
            return;
        m_Health -= damage;
        Debug.Log(damage);
        JointLimits newLimit = hinge.limits;
        newLimit.max = Mathf.Lerp(90.0f, 0.0f, m_Health / initialHealth);
        hinge.limits = newLimit;
    }

    IEnumerator DamageTimer()
    {
        onCooldown = true;
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
    void Update()
    {

    }
}
