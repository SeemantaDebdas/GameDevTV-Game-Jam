using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signs : MonoBehaviour
{
    [SerializeField] GameObject eventCanvas = null;
    [SerializeField] KeyCode keyCode;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eventCanvas.SetActive(true);
            collision.GetComponent<Player>().ControlInput(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(keyCode))
            {
                eventCanvas.SetActive(true);
                collision.GetComponent<Player>().ControlInput(true);
            }
        }
    }
}
