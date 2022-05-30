using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] GameObject warningCanvas;
    Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instructionCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instructionCanvas.SetActive(false);
            warningCanvas.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool isLinked = collision.GetComponent<SoulLink>().IsLinked;
            warningCanvas.SetActive(!isLinked);
            if (isLinked)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    anim.Play("DoorOpenAnimation");
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}
