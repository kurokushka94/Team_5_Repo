using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Vector3 offset;

    Vector3 position;
    Vector3 newPosition;

    private float increase;
    private bool isMoving;

    [HideInInspector] public bool isIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIncreasing)
        {
            increase = 0.01f;
        }
        else if (!isIncreasing)
        {
            increase = -0.01f;
        }

        newPosition = position + offset;

        if (Vector3.Distance(transform.position, newPosition) > 0.01f)
        {
            StartCoroutine("MovePlatform");
        }
    }

    IEnumerator MovePlatform()
    {
        isMoving = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + increase, transform.position.z);
        yield return new WaitForSecondsRealtime(0.01f);
        isMoving = false;
    }

    void AddToOffset(Vector3 _distanceToAdd)
    {
        offset += _distanceToAdd;
    }

    void SubtractFromOffset(Vector3 _distanceToSubtract)
    {
        offset -= _distanceToSubtract;
    }
}
