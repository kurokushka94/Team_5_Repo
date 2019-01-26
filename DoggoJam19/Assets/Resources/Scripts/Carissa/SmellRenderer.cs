using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmellRenderer : MonoBehaviour
{
    private GameObject target;
    NavMeshAgent agent;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);

            if (Vector3.Distance(transform.position, target.transform.position) <= 0.5f)
            {
                Destroy(gameObject,0.5f);
            }
        }
    }

    void SetTarget(GameObject _target)
    {
        target = _target;
    }
}
