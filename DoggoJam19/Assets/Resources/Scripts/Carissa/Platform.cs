using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Vector3 offset;

    Vector3 position;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = position + offset;
        transform.position = newPosition;
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
