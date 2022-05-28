using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] Transform deadBody;
    SoulLink soulLink;
    private void Start()
    {
        soulLink = FindObjectOfType<SoulLink>();
        soulLink.AllSoulsLinked += ActivateDeadBodyLink;
    }

    private void ActivateDeadBodyLink()
    {
        if (deadBody != null)
            deadBody.GetComponent<LinkableEntity>().enabled = true;
    }
}
