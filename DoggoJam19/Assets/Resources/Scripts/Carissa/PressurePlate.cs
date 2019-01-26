using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed;
    private bool hasLeftPlate;

    GameObject platform;

    Vector3 position;
    Vector3 pressedPos;
    Vector3 offset = new Vector3(0f, -0.075f, 0f);

    Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        platform = GameObject.Find("Platform");

        if (gameObject.name == "PressurePlate[5]")
        {
            distance = new Vector3(0f, 0.5f, 0f);
        }

        position = transform.position;
        pressedPos = position + offset;
        hasLeftPlate = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPressed)
        //{
        //    transform.position = pressedPos;
            
        //}
        //else if (!isPressed)
        //{
        //    transform.position = position;
            
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hasLeftPlate)
            {
                if (!isPressed)
                {
                    isPressed = true;
                    transform.position += offset;
                    platform.SendMessage("AddToOffset", distance, SendMessageOptions.DontRequireReceiver);
                }
                else if (isPressed)
                {
                    isPressed = false;
                    transform.position = position;
                    platform.SendMessage("SubtractFromOffset", distance, SendMessageOptions.DontRequireReceiver);
                }
                hasLeftPlate = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hasLeftPlate = true;
        }
    }
}
