using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableScript : MonoBehaviour
{

    public float rotSpeed;
    public GameObject winningScreen;

    private Vector3 rotVector;
    private bool killing;

    void Update()
    {
        rotVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotSpeed, transform.eulerAngles.z);
        transform.eulerAngles = rotVector;
        killing = false;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if(_collider.tag == "Player")
        {
            winningScreen.SendMessage("AddScore", SendMessageOptions.DontRequireReceiver);
            StartCoroutine("Kill");
        }
    }

    IEnumerator Kill()
    {
        killing = true;
        MeshRenderer[] meshes = transform.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshes.Length; ++i)
            meshes[i].enabled = false;

        BoxCollider[] colliders = transform.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < colliders.Length; ++i)
            colliders[i].enabled = false;

        yield return new WaitForSecondsRealtime(5);
        Destroy(this.gameObject);
        killing = false;
    }
}
