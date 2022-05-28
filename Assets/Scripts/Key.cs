using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum KeyType{
        blue,
        red
    };

    [SerializeField] KeyType keyType;
    
    public KeyType GetKeyType { get { return keyType; } }
}
