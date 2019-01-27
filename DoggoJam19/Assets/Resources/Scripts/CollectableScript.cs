using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{

    public float rotSpeed;

    private Vector3 rotVector;

    void Update()
    {
        rotVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotSpeed, transform.eulerAngles.z);
        transform.eulerAngles = rotVector;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.tag == "Player")
            _collider.SendMessage("AddScore", 1, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject, 0.2f);
    }
}
