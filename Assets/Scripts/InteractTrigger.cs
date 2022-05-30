using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] Interactable.InteractableType interactableType;
    [SerializeField] UnityEvent triggerEventList;

    public event Action OnTriggered;
    public event Action OnLeverTriggered;
    public event Action<bool> OnPressurePadChanged;

    Player player;

    private void Update()
    {
        gameObject.SetActive(!FindObjectOfType<SoulLink>().IsLinked);
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            triggerEventList.AddListener(EnablePlayerInput);
        }
    }

    void EnablePlayerInput()
    {
        if (player == null) return;
        player.ControlInput(false);
    }

    private void ActivatePressurePad()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        OnPressurePadChanged?.Invoke(true);
    }

    private void ActivateTrigger()
    {
        if (Input.GetKey(KeyCode.F))
        {
            OnTriggered?.Invoke();
            triggerEventList?.Invoke();
            //trigger dissolve shader
        }
    }

    void ActivateLeverTrigger()
    {
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
