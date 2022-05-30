using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signs : MonoBehaviour
{
    [SerializeField] GameObject eventCanvas = null;
    [SerializeField] KeyCode keyCode;

    bool called = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !called)
        {
            eventCanvas.SetActive(true);
            collision.GetComponent<Player>().ControlInput(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !called)
        {
            if (Input.GetKeyDown(keyCode))
            {
                called = true;
                eventCanvas.SetActive(true);
                collision.GetComponent<Player>().ControlInput(true);
            }
        }
    }
}
