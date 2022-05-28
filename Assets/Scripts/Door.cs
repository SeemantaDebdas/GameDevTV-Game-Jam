using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Key.KeyType keyType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KeyHolder keyHolder = collision.GetComponent<KeyHolder>();
        if (keyHolder == null) return;

        if (keyHolder.ContainsKey(keyType))
        {
            //play animation of door opening
            Destroy(gameObject);
        }
    }
}
