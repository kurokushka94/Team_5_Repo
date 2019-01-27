using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableScript : MonoBehaviour
{

    public float rotSpeed;
    private List<CollectableScript> collectables;

    private Vector3 rotVector;
    private Canvas gameWon;

    private int score;
    private bool won;
    private bool killing;
    private bool end;

    private void Start()
    {
        collectables = new List<CollectableScript>();
        CollectableScript[] collectablesArray = FindObjectsOfType<CollectableScript>();
        for (int i = 0; i < collectablesArray.Length; ++i)
            collectables.Add(collectablesArray[i]);
        gameWon = transform.GetComponentInChildren<Canvas>();
        won = false;
        killing = false;
    }

    void Update()
    {
        rotVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotSpeed, transform.eulerAngles.z);
        transform.eulerAngles = rotVector;

        if (score == 3)
        {
            if(!end)
            GameWon();
            won = true;
        }
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.tag == "Player")
            for(int i = 0; i < collectables.Count; ++i)
                collectables[i].SendMessage("AddScore", SendMessageOptions.DontRequireReceiver);

        if (!won)
        {
            for (int i = 0; i < collectables.Count; ++i)
                collectables[i].SendMessage("RemoveFromList", this.gameObject.GetComponent<CollectableScript>());

            MeshRenderer[] meshes = transform.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < transform.childCount; ++i)
                meshes[i].enabled = false;

            if (!killing)
                StartCoroutine("Kill");
        }

    }

    private void RemoveFromList(CollectableScript _toRemove)
    {
        collectables.Remove(_toRemove);
    }

    private void AddScore()
    {
        score += 1;
    }

    private void GameWon()
    {
        end = true;
        gameWon.enabled = true;
        Time.timeScale = 0;
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator Kill()
    {
        killing = true;
        yield return new WaitForSecondsRealtime(5);
        Destroy(this.gameObject, 5.0f);
        killing = false;
    }
}
