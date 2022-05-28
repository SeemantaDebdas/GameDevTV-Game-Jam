using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType{
        Trigger,
        Lever,
        PressurePad
    };

    [SerializeField] InteractableType interactableType;
    [SerializeField] InteractTrigger interactTrigger;

    [Header("Elevator")]
    [SerializeField] List<Transform> elevatorWaypoints;
    int currentElevatorWaypoint = 0;
    bool elevatorShouldMove = false;

    public InteractableType GetInteractableType { get { return interactableType; } }


    private void Start()
    {
        if(interactableType == InteractableType.Trigger)
            interactTrigger.OnTriggered += TriggerEvent;

        if(interactableType == InteractableType.PressurePad)
            interactTrigger.OnPressurePadChanged += PressurePadEvent;

        if (interactableType == InteractableType.Lever)
            interactTrigger.OnLeverTriggered += LeverEvent;
    }

    private void FixedUpdate()
    {
        if (elevatorShouldMove && elevatorWaypoints.Count > 0)
        {
            if (transform.position == elevatorWaypoints[currentElevatorWaypoint].position)
                currentElevatorWaypoint = (currentElevatorWaypoint + 1) % elevatorWaypoints.Count;

            transform.position =  Vector3.MoveTowards(transform.position, elevatorWaypoints[currentElevatorWaypoint].position, 2f * Time.deltaTime);
        }
    }

    private void TriggerEvent()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        if(transform.childCount > 0)
        {
            foreach(Transform child in transform)
                child.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    private void PressurePadEvent(bool args)
    {
        if (elevatorWaypoints.Count < 1) return;
        elevatorShouldMove = args;
    }

    void LeverEvent()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(interactableType == InteractableType.Trigger)
        {
            //trigger particle system
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                    child.parent = null;
            }
            Destroy(this.gameObject);
        }

        if(interactableType == InteractableType.PressurePad)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.parent = this.transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.parent = null;
    }
}
