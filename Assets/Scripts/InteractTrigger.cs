using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] Interactable.InteractableType interactableType;
    public event Action OnTriggered;
    public event Action OnLeverTriggered;
    public event Action<bool> OnPressurePadChanged;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactableType == Interactable.InteractableType.Trigger)
                ActivateTrigger();
            else if(interactableType == Interactable.InteractableType.Lever)
                ActivateLeverTrigger();
        }
        
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            if (interactableType == Interactable.InteractableType.PressurePad)
                ActivatePressurePad();
        }
        else
        {
            OnPressurePadChanged?.Invoke(false);
        }

    }

    private void ActivatePressurePad()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        OnPressurePadChanged?.Invoke(true);
    }

    private void ActivateTrigger()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        if (Input.GetKey(KeyCode.F))
        {
            OnTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }

    void ActivateLeverTrigger()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        if (Input.GetKey(KeyCode.F))
        {
            OnLeverTriggered?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GetComponent<SpriteRenderer>().color = Color.white;
        OnPressurePadChanged?.Invoke(false);
    }
}
