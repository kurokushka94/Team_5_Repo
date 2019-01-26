using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoizeIndicatorScript : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player)
            transform.LookAt(player.transform);
    }
}
