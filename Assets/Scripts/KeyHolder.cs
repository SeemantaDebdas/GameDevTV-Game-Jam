using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    List<Key.KeyType> keys = new List<Key.KeyType>();

    public void AddKey(Key.KeyType key) => keys.Add(key);

    public bool ContainsKey(Key.KeyType key) => keys.Contains(key);

    public void RemoveKey(Key.KeyType key) => keys.Remove(key);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GetComponent<SoulLink>().IsLinked) return;
        Key key = collision.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType);
            Destroy(collision.gameObject);
        }
    }
}
